 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackCal
{
    public static Vector3 Bezier(Vector3 selfPos,Vector3 targetPos,Vector3 referPos,float t)  
    {
        Vector3 selfRef = Vector3.Lerp(selfPos, referPos, t);
        Vector3 targetRef = Vector3.Lerp(referPos, targetPos, t);
        Vector3 finalPoint = Vector3.Lerp(selfRef,targetRef,t);
        //print("final"+finalPoint);
        
        return finalPoint;
    }
    
    
}
