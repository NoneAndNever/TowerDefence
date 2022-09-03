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
        damageMultiplier = 1;
        radiusMultiplier = 1;
        speedMultiplier = 1;
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
        if (!enemy) return;
        print("开始攻击"+enemy.name);
        GameObject gameObject = ObjectPool.GetInstance().GetObject(arrows[0]);
        gameObject.transform.position = transform.position;
        gameObject.GetComponent<Arrow>().SetTargetArrow(enemy.transform)
            .damage=Random.Range(atkDamageMin,atkDamageMax)*universalDamageMultiplier*damageMultiplier;
       
        atkTimer = 0;
    }

    


    

}


    
    
