using System;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    public EnemyController enemyPrefab;
    public int count;
    public float spawnInterval;
}
