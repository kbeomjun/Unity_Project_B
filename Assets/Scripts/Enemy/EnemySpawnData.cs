using System;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private int _count;
    [SerializeField] private float _spawnInterval;

    public EnemyData EnemyData => _enemyData;
    public EnemyController EnemyPrefab => _enemyPrefab;
    public int Count => _count;
    public float SpawnInterval => _spawnInterval;
}
