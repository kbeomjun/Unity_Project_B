using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public enum EnemyState
{
    Idle,
    Knockback,
    Dead
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyUI _enemyUI;

    private EnemyData _data;
    private float _currentHealth;
    private SplineContainer _spline;
    private float _progress = 0.0f;
    private float _splineLength;
    private EnemyState _state = EnemyState.Idle;
    private float _knockbackRemaining;
    private float _knockbackSpeed;

    public float Progress => _progress;
    public EnemyState State => _state;
    public event Action<EnemyController> OnDestroyed;

    public void Init(EnemyData data, SplineContainer spline)
    {
        _data = data;
        _spline = spline;
        _splineLength = _spline.CalculateLength();
        _currentHealth = _data.MaxHealth;
        _state = EnemyState.Idle;
        _enemyUI.Init(_currentHealth, _data.MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        if (_state == EnemyState.Dead) return;

        _currentHealth -= damage;
        _enemyUI.UpdateHealth(_currentHealth, _data.MaxHealth);
        PlayHitEffect();

        if (_currentHealth <= 0.0f)
        {
            Dead();
        }
    }

    public void ApplyKnockback(float distance, float duration)
    {
        if (_state == EnemyState.Dead) return;

        _state = EnemyState.Knockback;
        _knockbackRemaining = distance;
        _knockbackSpeed = distance / duration;
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

            case EnemyState.Knockback:
                MoveBackward();
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
        float distance = _data.Speed * Time.deltaTime;
        float progressDelta = distance / _splineLength;
        _progress += progressDelta;
    }

    private void MoveBackward()
    {
        float distance = _knockbackSpeed * Time.deltaTime;
        float progressDelta = distance / _splineLength;
        _progress = _progress < progressDelta ? 0.0f : _progress - progressDelta;
        _knockbackRemaining -= distance;

        if (_knockbackRemaining <= 0.0f)
        {
            _knockbackRemaining = 0.0f;
            _state = EnemyState.Idle;
        }
    }

    private void UpdatePosition()
    {
        _progress = Mathf.Clamp01(_progress);
        Vector3 nextPosition = _spline.EvaluatePosition(_progress);
        Vector3 direction = nextPosition - transform.position;
        transform.position = nextPosition;

        if (_state == EnemyState.Idle && Mathf.Abs(direction.x) > 0.001f)
        {
            _spriteRenderer.flipX = direction.x < 0.0f;
        }

        _animator.SetBool("IsMoving", true);
    }

    private void PlayHitEffect()
    {
        _spriteRenderer.DOKill();

        _spriteRenderer.color = Color.white;
        _spriteRenderer.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo);
    }

}
