using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public int Gold { get; private set; }
    public int HealthPoint { get; private set; }
    public int SkillPoint { get; private set; }
    
    public int Wave { get; private set; }

    private void Awake()
    {
        EventCenter.GetInstance().AddListener<int>(EventType.GoldChange,SetGold)
            .AddListener<int>(EventType.HealthPointChange,SetHealthPoint)
            .AddListener<int>(EventType.SkillPointChange,SetSkillPoint);
        

    }

    private void SetGold(int value) => Gold += value;
    private void SetHealthPoint(int value) => HealthPoint += value;
    private void SetSkillPoint(int value) => SkillPoint += value;
}
