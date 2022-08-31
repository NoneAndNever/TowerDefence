using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField]protected int atkDamageMin;             //攻击伤害(最小)
    [SerializeField]protected int atkDamageMax;             //攻击伤害(最大)
    [SerializeField]protected float atkSpeed;               //攻击速度 
    [SerializeField]protected float atkRadius;              //攻击半径
    [SerializeField]protected EDamageType damageType;       //伤害类型(物理/魔法/真实)
    [SerializeField]protected float atkTimer;
    [Header("增益相关")]
    [SerializeField]protected float damageMultiplier;
    [SerializeField]protected float radiusMultiplier;
    [SerializeField]protected float speedMultiplier;
    [Header("敌人")] 
    [SerializeField]protected ContactFilter2D targetFilter;
    [SerializeField]protected List<Collider2D> enemies;

    /*[Header("组件")]*/
    /*[SerializeField]protected Rigidbody _rigidbody;
    [SerializeField]protected Collider2D col;*/


    protected virtual void Atk(Collider2D enemy){}
    
    /// <summary>
    /// 攻击冷却结束后进行的攻击判定
    /// 在指定pos点以radius为半径进行目标搜索并攻击
    /// </summary>
    /// <param name="pos">攻击范围的中心</param>
    /// <param name="radius">攻击范围半径</param>
    protected void AtkCheck(Vector2 pos, float radius)
    {
        //如果攻击就绪，判断范围内是否存在敌人
        Physics2D.OverlapCircle(pos, radius, targetFilter, enemies);
        
        //不存在则直接返回
        if (enemies.Count == 0)
        {
            print("不存在");
            return ;
        }
        
        //存在唯一一个则直接攻击此目标
        if (enemies.Count == 1)
        { 
            Atk(enemies[0]);
            return ;
        }
        
        //存在多个则进行遍历，寻找距离终点最近的目标并进行攻击
        int minIndex = 0;
        int minPos = enemies[0].GetComponent<Enemy>().GetPosIndex();
        int curPos;
        for (int i = 0; i < enemies.Count; i++)
        {
            curPos = enemies[i].GetComponent<Enemy>().GetPosIndex();
            if (minPos <= curPos) continue;
            minIndex = i;
            minPos = curPos;
        }
        Atk(enemies[minIndex]);
    }
}
