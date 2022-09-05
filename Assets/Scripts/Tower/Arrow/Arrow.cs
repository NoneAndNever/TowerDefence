using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.U2D.Path;
using Random = UnityEngine.Random;

/// <summary>
/// 箭矢脚本，用于箭矢自身飞行
/// 使用：直接挂载在箭矢上
/// </summary>
public class Arrow : Shoot<Arrow>
{
    [SerializeField] private Vector3 referPoint;//贝塞尔曲线参考点
    [SerializeField] private float percent;     //插值百分比

    private void OnEnable()
    {
        percent = 0;//重置插值百分比
    }

    private void Update()
    {
        IsReachingTarget(EDamageType.Physical);//判断是否触碰到敌人
    }

    private void FixedUpdate()
    {
        //如果目标存在则实时更新目标位置
        if (target) targetPoint= target.position;
        
        //根据时间递增百分比
        if (percent<1) {percent += Time.fixedDeltaTime * (2 / (targetPoint - transform.position).magnitude);}

        //求出贝塞尔曲线的下一个位移点
        Vector3 nextPoint=TrackCal.Bezier(transform.position, targetPoint, referPoint, percent);
        
        //根据两点的矢量差值设置箭矢的旋转
        Vector2 dir = (nextPoint - transform.position).normalized;
        transform.rotation=Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg,Vector3.forward);
        
        //将箭矢设置成下一个目标点
        transform.position =nextPoint;
    }

    public override Arrow SetTarget(Transform target)
    {
        base.SetTarget(target);
        Vector3 selfPos = transform.position;
        Vector3 targetPos = target.position;
        referPoint = new Vector2(Mathf.Lerp(selfPos.x,targetPos.x,0.5f),
            (selfPos.y>targetPos.y? selfPos.y:targetPos.y)+Random.Range(0.75f,1.25f));
        return this;
    }
}