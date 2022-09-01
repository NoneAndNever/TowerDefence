using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.U2D.Path;
using Random = UnityEngine.Random;

/// <summary>
/// 法术球轨迹模拟
/// 使用：直接挂载在一个物体上，物体会往上升，再沿直线飞出
/// </summary>

public class Magicball : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 referPoint;
    [SerializeField] private float percent;
    [SerializeField] public float damage;
    
    private void OnEnable()
    {
        
    }
}
