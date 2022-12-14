using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.WSA;

public class Enemy : MonoBehaviour
{

    [Header("基本属性")]
    [SerializeField] private float healthPoint;         //(当前)生命值
    [SerializeField] private float hpDefault;      //(初始)生命值 等确定后会改为const
    [SerializeField] private float magicDefence;
    [SerializeField] private float physicalDefence;
    [SerializeField] private int gold;
    [SerializeField] private EEnemyType enemyType;

    [Header("移动及路径相关")]
    [SerializeField] private PathManager pathManager;    //路径管理者
    [FormerlySerializedAs("path")] [SerializeField] public PathPoints pathPoints;                   //当前路径
    [SerializeField] private Vector2 moveDirection;      //速度放向
    [SerializeField] private float speed = 1;              //移动速度
    [SerializeField] private Vector2 lastPathPoint;
    [SerializeField] private Vector2 nextPathPoint;

    #region 路径相关属性

    //距离下一个点的距离
    public float DistanceToNextPoint => ((Vector2)transform.position-nextPathPoint).magnitude;
    
    //位置索引(指向的是下一个路径点)
    public int NextPointIndex{get; private set; }
    
    //相对默认路径点的偏移坐标
    public Vector2 Offset{get; set; }

    #endregion
    

    #region Mono自带生命周期函数

    private void Awake()
    {
        gameObject.GetComponent<Attackable>().GetDamage += GetDamage;   //获取到Attackable脚本并对GetDamage委托添加函数
    }

    private void OnEnable()
    {
        healthPoint = hpDefault;                            //重置生命值
        Offset = new Vector2(0.2f, 0.5f);               //调试用偏移,后期会更改//////////////////////////////////////////
        pathPoints=pathManager.GetPath(0);                    //调试用路径,后期会更改/////////////////////////////////
        
        lastPathPoint = pathPoints[pathPoints.GetCount() - 1];          //使用一个变量存储上一个路径点的坐标,从而避免重复获取
        
        transform.position = lastPathPoint+Offset;//将自身移动到路径的最后一个节点位置(即起始点)
        NextPointIndex = pathPoints.GetCount()-2;                   //索引点更改为倒数第二个节点位置

        nextPathPoint = pathPoints[NextPointIndex];                 //使用一个变量存储上一个路径点的坐标,从而避免重复获取
        moveDirection = (nextPathPoint - lastPathPoint).normalized;
    }

    
    private void Update()
    {
        if(NextPointIndex>=0) //如果还没有抵达最后一个节点,则进行移动
            Move();
        else
            ReachDestination();
    }


    #endregion
    
    /// <summary>
    /// 受伤时被调用
    /// </summary>
    /// <param name="damage">伤害值</param>
    /// <param name="damageType">伤害类型(枚举)</param>
    private void GetDamage(float damage, EDamageType damageType)
    {
        //Todo:进行护甲统计及护甲相关Debuff计算,对传进来的伤害进行进一步计算
        float defence = damageType switch
        {
            EDamageType.Physical => physicalDefence,
            EDamageType.Magic => magicDefence,
            _ => 0
        };

        healthPoint -= (damage-defence);
        //生命值降为0以下,则把自身推回对象池,等待下次调用
        if (healthPoint<0 && gameObject.activeSelf)
        {
            //todo:广播事件
            //把自身死亡的信息广播
            EventCenter.GetInstance().BroadcastEvent<Transform>(EventType.EnemyDie, gameObject.transform);
            //加金币
            InGameManager.GetInstance().SetGold(+gold);
            //把自身推回池中
            ObjectPool.GetInstance().PushObject(gameObject);
        }
    }


    /// <summary>
    /// 移动时调用
    /// 寻找自身上一个路径点和下一个路径点的矢量从而求出速度方向
    /// 乘以speed变量得到当前速度
    /// </summary>

    private void Move()
    {
        /*Vector2 selfPos = transform.position;
        Vector2 desPos = _path.GetNode(posIndex).position;
        if ((desPos-selfPos).magnitude<0.3f)
        {
            print("distance小于0.2");
            posIndex--;
            if (posIndex<0) return;
            desPos = _path.GetNode(posIndex).position;
        }
        _dir = (desPos - selfPos).normalized;
        transform.Translate(_dir*speed*Time.deltaTime);*/
        if (DistanceToNextPoint<0.5f)
            //如果到下一个路径点的距离小于0.5f,则判断是抵达下一个目标点
            //目标点前进(索引减1)
        {
            print("distance小于0.2");
            NextPointIndex--;
            //如果索引减一后小于0,说明已抵达终点
            if (NextPointIndex<0)return;
            //否则重新更新路径点相关信息
            lastPathPoint = nextPathPoint;
            nextPathPoint = pathPoints[NextPointIndex];
            moveDirection = (nextPathPoint - lastPathPoint).normalized;
        }
        transform.Translate(moveDirection * (speed * Time.deltaTime));
    }
    
    private void ReachDestination()
    {
        int damage = enemyType switch
        {
            EEnemyType.Summon => 1,
            EEnemyType.Minion => 1,
            EEnemyType.Elite => 2,
            EEnemyType.Boss => 100
        };
        InGameManager.GetInstance().SetHealthPoint(-damage);
        ObjectPool.GetInstance().PushObject(gameObject);
    }

    
}
