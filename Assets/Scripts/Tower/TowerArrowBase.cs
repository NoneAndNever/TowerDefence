using System;
using System.Collections;
using System.Collections.Generic;
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
        if (atkTimer>atkSpeed*speedMultiplier)
            //当计时器大于攻击CD则开始攻击并重置Timer
        {
            Atk(ChooseEnemy());
            atkTimer = 0;
        }
        atkTimer += Time.deltaTime;
    }
    

    protected override void Atk(Collider2D enemy)
    {
        //Todo：攻击行为
        print("攻击"+enemy.name);
        //ObjectPool.Instance.GetObject(arrows[0]);
    }

    private Collider2D ChooseEnemy()
    {
        int index = 0;
        do
        {
            if (enemies[index])
                return enemies[index];
            enemies.RemoveAt(index);
        } while (enemies.Count!=0);

        return null;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        enemies.Add(col);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other);
    }
    
    /// <summary>
    /// 升级的时候将原本记录的敌人传递给新的塔
    /// </summary>
    /// <param name="oldEnemies"></param>
    private void LevelUp(List<Collider2D> oldEnemies)
    {
        enemies = oldEnemies;
    }
}


    
    
