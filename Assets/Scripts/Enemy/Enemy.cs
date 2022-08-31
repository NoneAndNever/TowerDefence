using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本属性")]
    [SerializeField] private float healthPoint;
    [SerializeField] private float hpDefault = 20;

    [Header("移动相关")]
    [SerializeField]private PathManager pathManager;
    [SerializeField]public Path _path;
    [SerializeField]private Vector2 _dir;
    [SerializeField]private int posIndex;
    [SerializeField]private int speed = 1;
    [SerializeField]private Vector2 offset;
    private void Awake()
    {
        gameObject.GetComponent<Attackable>().GetDamage += GetDamage;
    }

    private void OnEnable()
    {
        healthPoint = hpDefault;
        offset = new Vector2(0.2f, 0.5f);
        _path=pathManager.GetPath(0);
        transform.position = _path.GetNode(_path.GetCount()-1).position+(Vector3)offset;
        posIndex = _path.GetCount()-1;
    }

    
    private void Update()
    {
        if(posIndex>=0) 
            Move();
    }
    
    
    private void GetDamage(float damage, EDamageType damageType)
    {
        healthPoint -= damage;
        if (healthPoint<0)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }


    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }
    public int GetPosIndex()
    {
        return posIndex;
    }
    
    private void Move()
    {
        /*Vector2 selfPos = transform.position;
        Vector2 desPos = _path.GetNode(posIndex).position;
        if ((desPos-selfPos).magnitude<0.3f)
        {
            print("distance小于0.2");
            posIndex--;
            if (posIndex<0) return;
            desPos = _path.GetNode(posIndex).position;
        }
        _dir = (desPos - selfPos).normalized;
        transform.Translate(_dir*speed*Time.deltaTime);*/
        Vector2 selfPos = _path.GetNode(posIndex).position;
        Vector2 desPos = _path.GetNode(posIndex-1).position;
        if ((desPos-(Vector2)transform.position).magnitude<0.5f)
        {
            print("distance小于0.2");
            posIndex--;
            if (posIndex<0) return;
            desPos = _path.GetNode(posIndex).position;
        }
        _dir = (desPos - selfPos).normalized;
        transform.Translate(_dir*speed*Time.deltaTime);
    }
}
