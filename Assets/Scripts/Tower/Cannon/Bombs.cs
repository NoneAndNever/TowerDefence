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
public class Bombs : Shoot<Bombs>
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Vector2 referPoint;
    [SerializeField] private float percent;
    [SerializeField] private float time = 1;
    [SerializeField] private float g = -10;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    
 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        percent = 0;
    }

    private void Update()
    {
        IsReachingTarget(EDamageType.Physical);
    }

    private void FixedUpdate()
    {
        /*if (percent<1)
        {
            percent += Time.deltaTime * (2 / (target.position - transform.position).magnitude);
        }

        Vector2 nextPoint=TrackCal.Bezier(transform.position, target.position, referPoint, percent);
        Vector2 dir = (nextPoint - (Vector2)transform.position).normalized;
        transform.rotation=Quaternion.AngleAxis(Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg,Vector3.forward);
        transform.position =nextPoint;*/
        rb.velocity += new Vector2(0, g)*Time.deltaTime;    
        
    }


    public override Bombs SetTarget(Transform target)
    {
        base.SetTarget(target);
        this.target = target;
        Vector2 selfPos = transform.position;
        Vector2 targetPos = target.position;
        referPoint = new Vector2(Mathf.Lerp(selfPos.x,targetPos.x,0.5f),
            (selfPos.y>targetPos.y? selfPos.y:targetPos.y)+Random.Range(5.75f,6.25f));
        //print(referPoint);
        float v_x, v_y;
        v_x = (targetPos.x - selfPos.x)/time;
        v_y = -((selfPos.y - targetPos.y)+(float)(0.5 * g * time * time))/time;
        //v_y = Mathf.Sqrt(-2 * g * (referPoint.y - selfPos.y));
        rb.velocity = new Vector2(v_x, v_y);
        return this;
    }


    protected override void IsReachingTarget(EDamageType damageType)
    {
        
    }
}