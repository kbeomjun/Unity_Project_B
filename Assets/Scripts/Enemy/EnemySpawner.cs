using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private Transform _startPoint;

    private readonly HashSet<EnemyController> _activeEnemies = new();
    private Action _onWaveFinished;
    private bool _isSpawnFinished;

    public void StartWave(WaveData waveData, Action onWaveFinished)
    {
        _onWaveFinished = onWaveFinished;
        _isSpawnFinished = false;
        StartCoroutine(SpawnWave(waveData));
    }

    private IEnumerator SpawnWave(WaveData waveData)
    {
        foreach (EnemySpawnData spawnData in waveData.EnemySpawnDatas)
        {
            for (int i = 0; i < spawnData.Count; i++)
            {
                SpawnEnemy(spawnData.EnemyData, spawnData.EnemyPrefab);
                yield return new WaitForSeconds(spawnData.SpawnInterval);
            }
        }

        _isSpawnFinished = true;
    }

    private void SpawnEnemy(EnemyData enemyData, EnemyController enemyPrefab)
    {
        EnemyController enemy = Instantiate(enemyPrefab, _startPoint.position, Quaternion.identity);
        enemy.Init(enemyData, _spline);
        enemy.OnDestroyed += OnEnemyDestroyed;
        _activeEnemies.Add(enemy);
    }

    private void OnEnemyDestroyed(EnemyController enemy)
    {
        enemy.OnDestroyed -= OnEnemyDestroyed;
        _activeEnemies.Remove(enemy);
        CheckWaveFinished();
    }

    private void CheckWaveFinished()
    {
        if (!_isSpawnFinished || _activeEnemies.Count > 0) return;

        Action onWaveFinished = _onWaveFinished;
        _onWaveFinished = null;
        onWaveFinished?.Invoke();
    }

}
