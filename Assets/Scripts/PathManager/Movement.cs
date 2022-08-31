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
    public int speed = 20;
    
    private void OnEnable()
    {
        _path=pathManager.GetPath(0);
        transform.position = _path.GetNode(_path.GetCount()-1).position;
        posIndex = _path.GetCount()-2;

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

    private void Move()     //敌人移动                                                                                                                                                            
    {
        Vector2 selfPos = transform.position;
        Vector2 desPos = _path.GetNode(posIndex).position;
        if ((desPos-selfPos).sqrMagnitude<0.3f)
        {
            print("distance小于0.2");
            posIndex--;
            if (posIndex<0) return;
            desPos = _path.GetNode(posIndex).position;
        }
        _dir = (desPos - selfPos).normalized;
        transform.Translate(_dir*20*Time.deltaTime);
    }
}
