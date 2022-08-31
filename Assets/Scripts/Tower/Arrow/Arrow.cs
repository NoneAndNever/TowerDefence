using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.U2D.Path;
using Random = UnityEngine.Random;

/// <summary>
/// 弓箭轨迹模拟
/// 使用：直接挂载在一个物体上，物体就会像抛物线一样抛射出去
/// </summary>
public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 referPoint;
    [SerializeField] private float percent;
    [SerializeField] public float damage;
    
    private void OnEnable()
    {
        percent = 0;
    }

    private void Update()
    {
        IsReachTarget();
    }

    private void FixedUpdate()
    {
        if (percent<1)
        {
            percent += Time.deltaTime * (2 / (target.position - transform.position).magnitude);
        }

        Vector2 nextPoint=TrackCal.Bezier(transform.position, target.position, referPoint, percent);
        Vector2 dir = (nextPoint - (Vector2)transform.position).normalized;
        transform.rotation=Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg,Vector3.forward);
        transform.position = nextPoint;
    }


    public Arrow SetTarget(Transform target)
    {
        this.target = target;
        Vector2 selfPos = transform.position;
        Vector2 targetPos = target.position;
        referPoint = new Vector2(Mathf.Lerp(selfPos.x,targetPos.x,0.5f),
            (selfPos.y>targetPos.y? selfPos.y:targetPos.y)+Random.Range(0.75f,1.25f));
        //print(referPoint);
        return this;
    }

    /// <summary>
    /// 判断自身与目标的距离
    /// </summary>
    private void IsReachTarget()
    {
        if ((transform.position - target.position).magnitude > 0.05) return;//如果距离过远则直接返回
        print("碰到敌人");
        target.GetComponent<Attackable>().OnGetDamage(damage,EDamageType.Physical);
        ObjectPool.Instance.PushObject(gameObject);
    }
   
}