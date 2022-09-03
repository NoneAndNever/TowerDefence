using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] public float damage;
    [SerializeField] protected Vector3 enemyPoint;//如果敌人死亡,则记录敌人最终的位置
    /// <summary>
    /// 判断自身与目标的距离
    /// </summary>
    protected void IsReachTarget()
    {
        if (target.gameObject)
            enemyPoint = target.position;
        if ((transform.position - enemyPoint).magnitude > 0.05)return;
        //如果距离过近则视为碰到敌人,或者抵达终点
        if (target.gameObject)
        {
            print("碰到敌人");
            target.GetComponent<Attackable>().OnGetDamage(damage,EDamageType.Physical);
        }
        ObjectPool.GetInstance().PushObject(gameObject);
    }

}
