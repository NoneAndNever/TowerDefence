using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.U2D.Path;
using Random = UnityEngine.Random;

/// <summary>
/// 法术球轨迹模拟
/// 使用：直接挂载在一个物体上，物体会往上升，再沿直线飞出
/// </summary>

public class MagicBall : Shoot<MagicBall>
{
    [SerializeField] private float percent;
    [SerializeField] private bool isReady;

    private void OnEnable()
    {
        percent = 0;
        isReady = false;
    }
    
    private void Update()
    {
        //如果设定了目标则判断是否到达目标点
        if(isReady) IsReachingTarget(EDamageType.Magic);
    }
    
    private void FixedUpdate()
    {
        //如果没准备好直接返回
        if (!isReady) return;
        //准备好了递增percent
        if (percent<1) percent += 0.01f;
        //如果目标存在则实时更新目标位置
        if(target) targetPoint = target.position;
        //插值计算下一点坐标
        Vector3 nextPoint= Vector3.Lerp(transform.position,targetPoint,percent);
        //根据两点决定旋转
        Vector3 dir = (nextPoint - transform.position).normalized;
        transform.rotation=Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg,Vector3.forward);
        //设施魔法球位移
        transform.position =nextPoint;
    }

    public override MagicBall SetTarget(Transform target)
    {
        base.SetTarget(target);
        isReady = true;
        return this;
    }
    
}
