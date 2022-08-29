using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("基本属性")]
    protected int atkDamage;                 //攻击伤害
    protected float atkSpeed;                //攻击速度 
    protected float atkRadius;               //攻击半径
    protected EDamageType damageType;        //伤害类型(物理/魔法/真实)

    [Header("组件")]
    protected Rigidbody _rigidbody;
    protected Collider2D col;

}
