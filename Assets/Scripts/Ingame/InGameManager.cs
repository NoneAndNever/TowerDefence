using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameManager : SingletonMono<InGameManager>
{
    [SerializeField] private int gold;
    [SerializeField] private int healthPoint;
    [SerializeField] private int skillPoint;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI spText;

    [SerializeField] private LevelInfo levelInfo;
    public int Wave { get; set; }

    protected override void Awake()
    {
        base.Awake();
        gold = levelInfo.goldNum;
        goldText.text = gold.ToString();
        healthPoint = levelInfo.healthPoint;
        hpText.text = healthPoint.ToString();
        skillPoint = levelInfo.skillPoint;
        spText.text = skillPoint.ToString();
        Wave = 0;
    }

    /// <summary>
    /// 金币变化
    /// </summary>
    /// <param name="delta">花费金额,正数代表增加,负数代表减少</param>
    public void SetGold(int delta)
    {
        gold +=delta;
        goldText.text = gold.ToString();
    }

    public int GetGold() => gold;

    /// <summary>
    /// 生命值变化
    /// </summary>
    /// <param name="delta">变化量,正数代表增加,负数代表减少</param>
    public void SetHealthPoint(int delta)
    {
        healthPoint += delta;
        healthPoint = healthPoint < 0 ? 0 : healthPoint;
        hpText.text = healthPoint.ToString();
    }

    /// <summary>
    /// 技能点变化
    /// </summary>
    /// <param name="delta">变化量,正数代表增加,负数代表减少</param>
    public void SetSkillPoint(int delta)
    {
        skillPoint += delta;
        spText.text = skillPoint.ToString();
    }
    private void WaveBegin()
    {
        Wave++;
    }
}
