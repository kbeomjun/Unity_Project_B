using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private int _enemyCount = 10;
    [SerializeField] private float _spawnInterval = 0.5f;

    public void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        EnemyController enemy = Instantiate(_enemyPrefab, _startPoint.position, Quaternion.identity);
        enemy.Init(_spline);
    }

}
