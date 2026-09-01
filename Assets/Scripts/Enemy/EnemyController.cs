using System;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public enum EnemyState
{
    Idle,
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
    private EnemyState _state = EnemyState.Idle;
    
    public float Progress => _progress;
    public EnemyState State => _state;
    public event Action<EnemyController> OnDestroyed;

    public void Init(EnemyData data, SplineContainer spline)
    {
        _data = data;
        _spline = spline;
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

    public void ApplyKnockback(float knockback)
    {

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
        _progress += _data.Speed * Time.deltaTime;
        _progress = Mathf.Clamp01(_progress);
        
        Vector3 nextPosition = _spline.EvaluatePosition(_progress);
        Vector3 direction = nextPosition - transform.position;
        transform.position = nextPosition;

        if (Mathf.Abs(direction.x) > 0.001f)
        {
            _spriteRenderer.flipX = direction.x < 0;
        }

        _animator.SetBool("IsMoving", true);

        if(_progress >= 1.0f)
        {
            Dead();
        }
    }

    private void PlayHitEffect()
    {
        _spriteRenderer.DOKill();

        _spriteRenderer.color = Color.white;
        _spriteRenderer.DOColor(Color.red, 0.05f).SetLoops(2, LoopType.Yoyo);
    }

}
