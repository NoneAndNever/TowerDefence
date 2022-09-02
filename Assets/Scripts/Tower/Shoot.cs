using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] public float damage;
    
    /// <summary>
    /// 判断自身与目标的距离
    /// </summary>
    protected void IsReachTarget()
    {
        if ((transform.position - target.position).magnitude > 0.1) return;//如果距离过远则直接返回
        print("碰到敌人");
        target.GetComponent<Attackable>().OnGetDamage(damage,EDamageType.Physical);
        ObjectPool.GetInstance().PushObject(gameObject);
    }

}
