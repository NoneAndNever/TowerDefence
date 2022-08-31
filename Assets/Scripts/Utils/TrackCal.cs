using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrackCal
{
    public static Vector2 Bezier(Vector2 selfPos,Vector2 targetPos,Vector2 referPos,float t)
    {
        Vector2 selfRef = Vector2.Lerp(selfPos, referPos, t);
        Vector2 targetRef = Vector2.Lerp(referPos, targetPos, t);
        Vector2 finalPoint = Vector2.Lerp(selfRef,targetRef,t);
        //print("final"+finalPoint);
        
        return finalPoint;
    }
}
