using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]private PathManager pathManager;
    private Path _path;
    private Vector2 _dir;
    private int posIndex;
    public int speed = 1;
    public Vector2 offset;
    
    private void OnEnable()
    {
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
