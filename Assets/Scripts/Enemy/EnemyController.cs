using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Splines;

public enum EnemyState
{
    Idle,
    Dead,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyUI _enemyUI;

    private EnemyData _data;
    private StatusEffectController _statusEffectController;
    private EnemyVisualEffectController _visualEffectController;
    private float _currentHealth;
    private float _speed;
    private SplineContainer _spline;
    private float _splineLength;
    private float _progress = 0.0f;
    private EnemyState _state = EnemyState.Idle;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public StatusEffectController StatusEffectController => _statusEffectController;
    public float Progress => _progress;
    public EnemyState State => _state;
    public event Action<EnemyController> OnDestroyed;

    public void Init(EnemyData data, SplineContainer spline)
    {
        _data = data;
        _statusEffectController = GetComponent<StatusEffectController>();
        _statusEffectController.Init(this);
        _visualEffectController = GetComponent<EnemyVisualEffectController>();
        _visualEffectController.Init(_spriteRenderer);
        _currentHealth = _data.MaxHealth;
        _speed = _data.Speed;
        _spline = spline;
        _splineLength = _spline.CalculateLength();
        _progress = 0.0f;
        _state = EnemyState.Idle;
        _enemyUI.Init(_currentHealth, _data.MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (_state == EnemyState.Dead) return;

        _currentHealth -= damage;
        _enemyUI.UpdateHealth(_currentHealth, _data.MaxHealth);
        _visualEffectController.PlayHitEffect();

        if (_currentHealth <= 0.0f)
        {
            Dead();
        }
    }

    public void MoveBackward(float distance)
    {
        if (_state == EnemyState.Dead) return;

        float progressDelta = distance / _splineLength;
        _progress = Mathf.Max(0.0f, _progress - progressDelta);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        _speed = _data.Speed * multiplier;
    }

    private void Dead()
    {
        if (_state == EnemyState.Dead) return;

        _state = EnemyState.Dead;
        _spriteRenderer.DOKill();
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                MoveForward();
                break;
        }

        UpdatePosition();

        if (_progress >= 1.0f)
        {
            Dead();
        }
    }

    private void MoveForward()
    {
        float distance = _speed * Time.deltaTime;
        float progressDelta = distance / _splineLength;
        _progress += progressDelta;
    }

    private void UpdatePosition()
    {
        _progress = Mathf.Clamp01(_progress);
        Vector3 nextPosition = _spline.EvaluatePosition(_progress);
        transform.position = nextPosition;

        if (_state == EnemyState.Idle)
        {
            Vector3 direction = (Vector3)_spline.EvaluatePosition(Mathf.Clamp01(_progress + 0.001f)) - nextPosition;

            if (Mathf.Abs(direction.x) > 0.001f)
            {
                _spriteRenderer.flipX = direction.x < 0.0f;
            }
        }

        _animator.SetBool("IsMoving", true);
    }

    public void PlaySlowEffect()
    {
        _visualEffectController.SetSlow(true);
    }

    public void StopSlowEffect()
    {
        _visualEffectController.SetSlow(false);
    }

}
