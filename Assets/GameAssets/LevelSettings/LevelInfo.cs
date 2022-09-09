using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level")]
public class LevelInfo : ScriptableObject
{
    public int id;              //当前是第几关
    public List<Wave> waves;    //关卡信息
    public int goldNum;
    public int healthPoint;
    public int skillPoint;
    [EnumFlags]public EAvailableTowerClass availableTowerClass;
    public EAvailableTowerLevel availableTowerLevel;
    
}

/// <summary>
/// 描述每一波次的信息
/// </summary>
[Serializable]
public class Wave
{
    // public int index;               //当前是第几波
    public List<WaveInfo> waveInfo; //当前波有哪些敌人
    public float startTime;         //当前波次的触发时间
    public int bonus;             //提前召唤波次的奖金
}

/// <summary>
/// 描述每一波次中的敌人分布安排
/// </summary>
[Serializable]
public class WaveInfo
{
    public GameObject enemyType;    //敌人prefab
    public int pathIndex;           //行走路径
    public int number;              //敌人数量
    public float time;              //敌人生成的时间
}

public enum EAvailableTowerLevel
{
    Base,
    Medium,
    Advanced,
    Expert
}


public enum EAvailableTowerClass
{
    Arrow = 1,
    Soldier = 2,
    Magic = 4,
    Cannon = 8
}