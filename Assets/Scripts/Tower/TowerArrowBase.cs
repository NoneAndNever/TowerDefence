using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class TowerArrowBase : TowerBase
{
    public static float universalDamageMultiplier = 1;
    
    [SerializeField]private List<GameObject> arrows;
    
    //每当从对象池中启用时，重置所有Multiplier
    private void OnEnable()
    {
        //重置所有倍率
        damageMultiplier = 1;
        radiusMultiplier = 1;
        speedMultiplier = 1;
        //初始化攻击cd
        atkTimer = atkSpeed * speedMultiplier;
    }

    private void Update()
    {
        
        if (atkTimer<atkSpeed*speedMultiplier)  //如果攻击计时器小于攻击CD则进行CD冷却
            atkTimer += Time.deltaTime;
        else                                    //否则进行攻击检查
            Atk(AtkCheck(transform.position,atkRadius*radiusMultiplier));
        //Debug.DrawLine(transform.position,transform.position+Vector3.left * (atkRadius * radiusMultiplier),Color.red,1);
    }
    

    protected override void Atk(Collider2D enemy)    //攻击
    {
        if (!enemy) return;//如果目标为空则返回
        //从对象池中取出对象
        GameObject gameObject = ObjectPool.GetInstance().GetObject(arrows[0]);
        //设置为箭塔当前的坐标
        gameObject.transform.position = transform.position;
        //设置箭矢射向的目标和箭矢的伤害
        gameObject.GetComponent<Arrow>().SetTarget(enemy.transform)
            .damage=Random.Range(atkDamageMin,atkDamageMax)*universalDamageMultiplier*damageMultiplier;
       //重置攻击计时器
        atkTimer = 0;
    }

    


    

}


    
    
