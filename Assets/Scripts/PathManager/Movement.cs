using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]private PathManager navigation;
    private Path _path;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _dir;
    private int posIndex;
    public int speed = 20;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _path=navigation.GetPath(0);
        transform.position = _path.GetNode(0).position;
        posIndex = 1;

    }

    private void Update()
    {
        if(posIndex<_path.GetCount()) 
            Move();
    }

    private void Move()
    {
        Vector2 selfPos = transform.position;
        Vector2 desPos = _path.GetNode(posIndex).position;
        float distance = Mathf.Pow(selfPos.x - desPos.x, 2) + Mathf.Pow(selfPos.y - desPos.y, 2);
        if ( distance< 2)
        {
            Debug.Log("distance小于2");
            posIndex++;
            if (posIndex==_path.GetCount()) return;
            desPos = _path.GetNode(posIndex).position;
        }
        _dir = (desPos - selfPos).normalized;
        _rigidbody2D.velocity = speed * Time.deltaTime*_dir;
    }
}
