using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rangeCircle;
    [SerializeField] private CapsuleCollider2D _placementCollider;
    [SerializeField] private LayerMask _enemyLayer;

    private TowerData _towerData;
    private float _range;
    private EnemyController _target;
    private float _attackSpeed = 1.0f;
    private float _attackTimer = 0.0f;
    private TowerAttackController _attackController;
    private bool _isAttacking = false;

    public void Init(TowerData data)
    {
        _towerData = data;
        _rangeCircle.localScale = Vector3.one * data.Range;
        _range = _rangeCircle.GetComponent<SpriteRenderer>().bounds.extents.x;
        _rangeCircle.gameObject.SetActive(true);
        _attackTimer = 0.0f;
        _attackController = GetComponent<TowerAttackController>();
        _attackController.Init();
        _isAttacking = false;
    }

    private void UpdateTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _range, _enemyLayer);

        EnemyController closestEnemy = null;
        float closestProgress = -1.0f;

        foreach (Collider2D enemyCollider in enemies)
        {
            EnemyController enemy = enemyCollider.GetComponentInParent<EnemyController>();

            if (enemy == null) continue;

            float progress = enemy.Progress;

            if (progress > closestProgress)
            {
                closestProgress = progress;
                closestEnemy = enemy;
            }
        }

        _target = closestEnemy;
    }

    private void LookAtTarget()
    {
        Vector3 direction = _target.transform.position - transform.position;

        if (Mathf.Abs(direction.x) > 0.001f)
        {
            _spriteRenderer.flipX = direction.x < 0.0f;
        }
    }

    private void UpdateAttackCooldown()
    {
        if (_attackTimer <= 0.0f) return;

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0.0f)
        {
            _attackTimer = 0.0f;
        }
    }

    private void Attack()
    {
        if (_isAttacking) return;

        Debug.Log($"Attack");
        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    public void FireProjectile()
    {
        _attackController.Attack(_target);
    }

    public void EndAttack()
    {
        _attackTimer = _towerData.AttackCooldown / _attackSpeed;
        _isAttacking = false;
    }

    private void Update()
    {
        UpdateTarget();
        UpdateAttackCooldown();

        if (_target != null)
        {
            LookAtTarget();

            if(_attackTimer <= 0.0f && !_isAttacking)
            {
                Attack();
            }
        }

    }

}
