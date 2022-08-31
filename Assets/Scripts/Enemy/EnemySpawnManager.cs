using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private PathManager PathManager;
    [SerializeField] private List<Vector2> offsets;
    [SerializeField] private List<GameObject> gameObjects;

    public void SpawnEnemy()
    {
        ObjectPool.Instance.GetObject(gameObjects[0]).GetComponent<Enemy>().SetOffset(Vector2.zero);
    }
}
