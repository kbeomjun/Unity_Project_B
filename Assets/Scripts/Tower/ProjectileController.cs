using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 15.0f;

    private SpriteRenderer _spriteRenderer;
    private EnemyController _target;

    public void Init(EnemyController target)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _target = target;
    }

    private void HitTarget()
    {
        Debug.Log($"Hit Target: {_target.name}");
        Destroy(gameObject);
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (_target.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

        if (Mathf.Abs(direction.x) > 0.001f)
        {
            _spriteRenderer.flipX = direction.x < 0.0f;
        }

        if (Vector3.Distance(transform.position, _target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }

}
