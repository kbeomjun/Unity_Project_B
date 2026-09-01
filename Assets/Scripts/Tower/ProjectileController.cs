using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 15.0f;

    private SpriteRenderer _spriteRenderer;
    private TowerController _attacker;
    private EnemyController _target;
    private List<AttackEffect> _effects;

    public void Init(TowerController attacker, EnemyController target, List<AttackEffect> effects)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _attacker = attacker;
        _target = target;
        _effects = effects;
    }

    private void HitTarget()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        foreach (AttackEffect effect in _effects)
        {
            if (effect == null) continue;

            effect.Apply(_attacker, _target);
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if (_target == null || _target.State == EnemyState.Dead)
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
