using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有炮塔的投掷物基类
/// </summary>
public class Shoot<T> : MonoBehaviour where T : Shoot<T>
{
    [SerializeField] public float damage;           //记录投掷物造成的伤害
    [SerializeField] protected Transform target;    //记录投掷物的目标
    [SerializeField] protected Vector3 targetPoint;  //记录目标的坐标信息，如果目标死亡,则记录目标最终的位置
    
    
    /// <summary>
    /// 通过检查自身与目标的距离 来判断是否到达目标点
    /// </summary>
    protected virtual void IsReachingTarget(EDamageType damageType)
    {
        //目标是否存在，存在则实时更新对象位置，不存在则记录对象最后出现位置
        if(target) targetPoint = target.position;
        //如果距离过近则视为碰到敌人,或者抵达终点
        if ((transform.position - targetPoint).magnitude > 0.05)return;
        //如果抵达目标位置并且目标仍存在，则调用Attackable脚本对目标造成伤害
        if (target)
        {
            target.GetComponent<Attackable>().OnGetDamage(damage,damageType);
            //todo:播放特效
        }
        else
        {
            //todo:淡出特效等
        }
        //如果抵达目标点，则移除监听，自身退回池中
        EventCenter.GetInstance().RemoveListener<Transform>(EventType.EnemyDie,IsTargetKilled);
        ObjectPool.GetInstance().PushObject(gameObject);
    }

    /// <summary>
    /// 如果目标被消灭，则移除目标信息
    /// </summary>
    /// <param name="target">被消灭的目标的Transform</param>
    protected virtual void IsTargetKilled(Transform target)
    {
        if(this.target==target) this.target = null;
        // 进行判断，如果被消灭的目标是投掷物原本指向的坐标，则清空投掷物指向目标的信息。
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">目标的Transform</param>
    /// <returns>本身脚本的Type</returns>
    public virtual T SetTarget(Transform target)
    {
        //将箭矢的目标设置成传入的对象
        this.target = target;
        //添加对目标的监听，如果目标死亡，则调用IsTargetKilled清空目标
        EventCenter.GetInstance().AddListener<Transform>(EventType.EnemyDie, IsTargetKilled);
        return this as T;
    }
}
