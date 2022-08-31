using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public event Action<float, EDamageType> GetDamage;

    /// <summary>
    /// 攻击者调用被攻击者的此函数
    /// </summary>
    /// <param name="damage">攻击方经过计算加成后的伤害</param>
    /// <param name="damageType">攻击方造成的伤害类型</param>
    public void OnGetDamage(float damage, EDamageType damageType)
    {
        GetDamage?.Invoke(damage,damageType);
    }
    
}
