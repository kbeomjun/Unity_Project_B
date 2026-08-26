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
        foreach (EnemySpawnData spawnData in waveData.enemySpawnDatas)
        {
            for (int i = 0; i < spawnData.count; i++)
            {
                SpawnEnemy(spawnData.enemyPrefab);
                yield return new WaitForSeconds(spawnData.spawnInterval);
            }
        }

        _isSpawnFinished = true;
    }

    private void SpawnEnemy(EnemyController enemyPrefab)
    {
        EnemyController enemy = Instantiate(enemyPrefab, _startPoint.position, Quaternion.identity);
        enemy.Init(_spline, OnEnemyDestroyed);
        _activeEnemies.Add(enemy);
    }

    private void OnEnemyDestroyed(EnemyController enemy)
    {
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
