using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 泛型基类:用于创建单例模式
/// </summary>
/// <typeparam name="T">类型</typeparam>
public class BaseManager<T> where T : new()
{
    private static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = new T();
        }
        return _instance;
    }
}
