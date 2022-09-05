using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField] protected int atkDamageMin;             //攻击伤害(最小)
    [SerializeField] protected int atkDamageMax;             //攻击伤害(最大)
    [SerializeField] protected float atkSpeed;               //攻击速度 
    [SerializeField] protected float atkRadius;              //攻击半径
    [SerializeField] protected EDamageType damageType;       //伤害类型(物理/魔法/真实)
    [SerializeField] protected float atkTimer;

    [Header("增益相关")] 
    [SerializeField] protected float damageMultiplier = 1;      //攻击倍率
    [SerializeField] protected float radiusMultiplier = 1;      //半径倍率
    [SerializeField] protected float speedMultiplier = 1;       //速度倍率
    [SerializeField] [EnumFlags] protected ETowerStatus towerStatus;

    [Header("敌人")] 
    [SerializeField]protected ContactFilter2D targetFilter;     //目标过滤器
    [SerializeField]protected List<Collider2D> enemies;
    
    protected virtual void Atk(Collider2D enemy){}
    
    /// <summary>
    /// 攻击冷却结束后进行的攻击判定
    /// 在指定pos点以radius为半径进行目标搜索并攻击
    /// </summary>
    /// <param name="pos">攻击范围的中心</param>
    /// <param name="radius">攻击范围半径</param>
    protected Collider2D AtkCheck(Vector2 pos, float radius)
    {
        //如果攻击就绪，判断范围内是否存在敌人
        Physics2D.OverlapCircle(pos, radius, targetFilter, enemies);
        
        switch (enemies.Count)
        {
            //不存在则直接返回
            case 0:
                print("不存在");
                return null;
            //存在唯一一个则直接攻击此目标
            case 1:
                return enemies[0];
        }

        //存在多个则进行遍历，寻找距离终点最近的目标并进行攻击
        int minEnemyIndex = 0;
        int minPointIndex = enemies[0].GetComponent<Enemy>().NextPointIndex;    //最近是第一个敌人
        for (int i = 1; i < enemies.Count; i++)                                             
        {
            var curPointIndex = enemies[i].GetComponent<Enemy>().NextPointIndex;
            
            if (minPointIndex < curPointIndex) continue;      //如果第i个敌人索引更往前
            if (minPointIndex == curPointIndex)
            {
                if (enemies[i].GetComponent<Enemy>().DistanceToNextPoint > enemies[minEnemyIndex].GetComponent<Enemy>().DistanceToNextPoint)
                    continue;
            }
            minEnemyIndex = i;
            minPointIndex = curPointIndex;
        }
        return  enemies[minEnemyIndex];
    }
}
