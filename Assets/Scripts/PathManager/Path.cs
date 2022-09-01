using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    //当前路径的路径点列表
    [SerializeField]private List<Transform> pointList = new List<Transform>();
    //利用索引器访问列表中的路径点
    public Vector2 this[int index] => pointList[index].position;
    //获取列表路径点数量
    public int GetCount() => pointList.Count;
    
}
