using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager:MonoBehaviour
{
    public List<Path> pathManager = new List<Path>();
    
    public Path GetPath(int index)  
    {
        return pathManager[index];
    }
}
