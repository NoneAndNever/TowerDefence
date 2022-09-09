using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : SingletonAutoMono<EnemySpawnManager>
{
    [SerializeField] private PathManager PathManager;
    [SerializeField] private List<Vector2> offsets;
    [SerializeField] private List<GameObject> gameObjects;
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private List<float> startTime;
    [SerializeField] private List<int> bonus;
    [SerializeField] private float waveTimer;   //计时器
    [SerializeField] private List<WaveInfo> waveInfo;
    private const int delayTime = 40;   //每波标志出现到敌人自动出现的时间

    public void SpawnEnemy()
    {
        ObjectPool.GetInstance().GetObject(gameObjects[0]).GetComponent<Enemy>().Offset=Vector2.zero;
    }

    public void Awake()
    {
        waves = levelInfo.waves;
        /*for (int i = 0; i < waves.Count; i++)
        {
            bonus[i] = waves[i].bonus;
            startTime[i] = waves[i].startTime;
        }*/
    }

    public void Update()
    {
        waveTimer += Time.deltaTime;
        
        int presentWave = InGameManager.GetInstance().Wave; //当前波次，即为下波次的下标
        if (waveTimer >= waves[presentWave].startTime)  //计时器超过下波时间，开始记录下拨信息
        {
            waveInfo = waves[presentWave].waveInfo;     //敌人信息
            bonus[presentWave] = waves[presentWave].bonus;  //此波的奖金
        }

        if (waveTimer >= waves[presentWave].startTime + delayTime)
        {
            //根据waveinfo出怪
        }
    }


    public void EarlyCalling(int presentWave)
    {
        waveTimer = waves[presentWave].startTime + delayTime;
    }
    
}
