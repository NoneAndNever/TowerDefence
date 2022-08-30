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
    
}
