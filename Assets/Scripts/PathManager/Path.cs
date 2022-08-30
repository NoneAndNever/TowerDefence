using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Path
{
    [SerializeField]private List<Transform> pointList = new List<Transform>();
    
    public Transform GetNode(int index)
    {
        return pointList[index];
    }

    public int GetCount()
    {
        return pointList.Count;
    }
}
