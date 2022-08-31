using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerArrowBase : TowerBase
{
    public static float universalDamageMultiplier = 1;
    [SerializeField]private float atkTimer;
    [SerializeField]private List<GameObject> arrows;
    
    //每当从对象池中启用时，重置所有Multiplier
    private void OnEnable()
    {
        damageMultiplier = 1;
        radiusMultiplier = 1;
        speedMultiplier = 1;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position,transform.position+Vector3.left * (atkRadius * radiusMultiplier),Color.red,1);
        if (atkTimer>atkSpeed*speedMultiplier)
            //当计时器大于攻击CD则开始攻击并重置Timer
        {
            Atk(ChooseEnemy(transform.position,atkRadius*radiusMultiplier));
            atkTimer = 0;
        }
        atkTimer += Time.deltaTime;
    }
    

    protected override void Atk(Collider2D enemy)    //攻击
    {
        print("开始攻击");
        if(!enemy) return;
        //Todo：攻击行为
        print("攻击"+enemy.name);
        //ObjectPool.Instance.GetObject(arrows[0]);
    }

    private Collider2D ChooseEnemy(Vector2 pos, float radius)      //选择敌人  
    {
        Physics2D.OverlapCircle(pos, radius, targetFilter, enemies);
        if (enemies.Count == 0) return null;
        if (enemies.Count == 1) return enemies[0];
        int minIndex = 0;
        int minPos = enemies[0].GetComponent<Movement>().GetPosIndex();
        int curPos;
        for (int i = 0; i < enemies.Count; i++)
        {
            curPos = enemies[i].GetComponent<Movement>().GetPosIndex();
            if (minPos <= curPos) continue;
            minIndex = i;
            minPos = curPos;
        }
        return enemies[minIndex];
    }
    

    

}


    
    
