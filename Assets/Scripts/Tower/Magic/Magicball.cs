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

public class Magicball : Shoot
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 referPoint;
    [SerializeField] private float percent;
    private bool isReady;

    private void OnEnable()
    {
        percent = 0;
        isReady = false;
        Invoke("SetReady",0.6f);
    }
    
    private void Update()
    {
        if (isReady)
            IsReachTarget();
    }
    
    private void FixedUpdate()
    {
        if (!isReady) return;
        if (percent<1)
        {
            percent += 0.01f;
        }
        Vector2 nextPoint= Vector2.Lerp(transform.position,target.position,percent);
        Vector2 dir = (nextPoint - (Vector2)transform.position).normalized;
        transform.rotation=Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg,Vector3.forward);
        transform.position =nextPoint;
    }

    public Magicball SetTargetMagic(Transform target)
    {
        this.target = target;
        Vector2 selfPos = transform.position;
        Vector2 targetPos = target.position;
        //print(referPoint);
        return this;
    }

    public void SetReady()
    {
        isReady = true;
    }
}
