using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private WaveData[] _waveDatas;

    private int _currentWave;
    private int _maxWave;
    private bool _isWaveRunning = false;

    private void Awake()
    {
        _currentWave = 0;
        _maxWave = _waveDatas.Length;
        _waveText.text = $"Wave {_currentWave + 1}/{_maxWave}";
    }

    public void OnClickWaveStartButton()
    {
        if (_isWaveRunning || _currentWave >= _maxWave) return;

        WaveData waveData = _waveDatas[_currentWave];
        _isWaveRunning = true;
        _enemySpawner.StartWave(waveData, OnWaveFinished);
    }

    private void OnWaveFinished()
    {
        _currentWave++;
        _isWaveRunning = false;

        if(_currentWave >= _maxWave)
        {
            _waveText.text = $"Stage Clear";
            return;
        }

        _waveText.text = $"Wave {_currentWave + 1}/{_maxWave}";
    }

}
