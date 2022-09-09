using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager:SingletonMono<PathManager>
{
    public List<PathPoints> pathManager = new List<PathPoints>();
    
    public PathPoints GetPath(int index)  
    {
        return pathManager[index];
    }
}
