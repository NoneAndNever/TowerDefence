using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("基本属性")]
    protected int atkDamageMin;              //攻击伤害(最小)
    protected int atkDamageMax;              //攻击伤害(最大)
    protected float atkSpeed;                //攻击速度 
    protected float atkRadius;               //攻击半径
    protected EDamageType damageType;        //伤害类型(物理/魔法/真实)

    [Header("组件")]
    protected Rigidbody _rigidbody;
    protected Collider2D col;

    protected virtual void Atk(GameObject Enemy)
    {
        ;
    }
    protected GameObject ChooseEnemy()
    {
     //Todo:检测范围内离目标点最近的敌人并返回   
     return null;
    }

}
