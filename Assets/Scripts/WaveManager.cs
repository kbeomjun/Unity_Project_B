using DG.Tweening;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private GameObject _waveStartButton;
    [SerializeField] private GameObject _waveFastButton;
    [SerializeField] private ActiveRing _waveFastActiveRing;
    [SerializeField] private GameObject _waveContinueButton;
    [SerializeField] private ActiveRing _waveContinueActiveRing;
    [SerializeField] private WaveData[] _waveDatas;

    private int _currentWave;
    private int _maxWave;
    private bool _isWaveRunning = false;
    private bool _isDoubleSpeed = false;
    private bool _isWaveContinue = false;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _currentWave = 0;
        _maxWave = _waveDatas.Length;
        _waveText.text = $"Wave {_currentWave + 1}/{_maxWave}";
        _isWaveRunning = false;
        _isDoubleSpeed = false;
        _isWaveContinue = false;
        Time.timeScale = 1.0f;
        _waveStartButton.SetActive(true);
        _waveFastButton.SetActive(false);
        _waveFastActiveRing.StopActiveAnimation();
        _waveContinueActiveRing.StopActiveAnimation();
    }

    public void OnClickWaveStartButton()
    {
        if (_isWaveRunning || _currentWave >= _maxWave) return;

        if (!_isWaveContinue || (_isWaveContinue && !_isWaveRunning))
        {
            _waveStartButton.SetActive(false);
            _waveFastButton.SetActive(true);
        }

        WaveData waveData = _waveDatas[_currentWave];
        _isWaveRunning = true;
        _enemySpawner.StartWave(waveData, OnWaveFinished);
    }

    private void OnWaveFinished()
    {
        _isWaveRunning = false;
        _currentWave++;

        if (_currentWave >= _maxWave)
        {
            _waveText.text = "Stage Clear";
            _isDoubleSpeed = false;
            _isWaveContinue = false;
            Time.timeScale = 1.0f;
            _waveStartButton.SetActive(true);
            _waveFastButton.SetActive(false);
            _waveFastActiveRing.StopActiveAnimation();
            _waveContinueActiveRing.StopActiveAnimation();
            return;
        }

        _waveText.text = $"Wave {_currentWave + 1}/{_maxWave}";

        if (!_isWaveContinue)
        {
            _isDoubleSpeed = false;
            Time.timeScale = 1.0f;
            _waveStartButton.SetActive(true);
            _waveFastButton.SetActive(false);
            _waveFastActiveRing.StopActiveAnimation();
        }
        else
        {
            OnClickWaveStartButton();
        }
    }

    public void OnClickWaveFastButton()
    {
        _isDoubleSpeed = !_isDoubleSpeed;

        if (_isDoubleSpeed)
        {
            Time.timeScale = 2.0f;
            _waveFastActiveRing.PlayActiveAnimation();
        }
        else
        {
            Time.timeScale = 1.0f;
            _waveFastActiveRing.StopActiveAnimation();
        }
    }

    public void OnClickWaveContinueButton()
    {
        _isWaveContinue = !_isWaveContinue;

        if (_isWaveContinue)
        {
            _waveContinueActiveRing.PlayActiveAnimation();
        }
        else
        {
            _waveContinueActiveRing.StopActiveAnimation();
        }
    }

}
