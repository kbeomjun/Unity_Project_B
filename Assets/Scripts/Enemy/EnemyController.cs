using System;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    
    private SplineContainer _spline;
    private float _speed = 0.05f;
    private float _progress = 0.0f;
    public float Progress => _progress;
    private Action<EnemyController> _onDestroyed;

    public void Init(SplineContainer spline, Action<EnemyController> onDestroyed)
    {
        _spline = spline;
        _onDestroyed = onDestroyed;
    }

    private void Die()
    {
        _onDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    private void Update()
    {
        _progress += _speed * Time.deltaTime;
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
            Die();
        }
    }

}
