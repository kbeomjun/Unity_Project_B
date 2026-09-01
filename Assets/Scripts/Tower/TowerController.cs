using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rangeCircle;
    [SerializeField] private CapsuleCollider2D _placementCollider;
    [SerializeField] private Transform _rightFirePoint;
    [SerializeField] private Transform _leftFirePoint;

    private TowerData _data;
    private TowerStats _stats;
    private float _range;
    private List<EnemyController> _targets;
    private float _attackSpeed = 1.0f;
    private float _attackTimer = 0.0f;
    private TowerTargetFinder _targetFinder;
    private TowerAttack _towerAttack;
    private bool _isAttacking = false;

    public TowerData Data => _data;
    public TowerStats Stats => _stats;

    public void Init(TowerData data)
    {
        _data = data;
        _stats = new TowerStats(data);
        _rangeCircle.localScale = Vector3.one * _stats.Range;
        _range = _rangeCircle.GetComponent<SpriteRenderer>().bounds.extents.x;
        _rangeCircle.gameObject.SetActive(false);
        _targets = new List<EnemyController>();
        _attackTimer = 0.0f;
        _targetFinder = GetComponent<TowerTargetFinder>();
        _targetFinder.Init(_stats);
        _towerAttack = GetComponent<TowerAttack>();
        _towerAttack.Init(this);
        _isAttacking = false;
    }

    private void UpdateTarget()
    {
        _targets = _targetFinder.FindTargets();
    }

    private void LookAtTarget()
    {
        if (_targets.Count == 0 || _stats.TargetCountType == TargetCountType.All) return;

        Vector3 direction = _targets[0].transform.position - transform.position;
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

        _isAttacking = true;
        _animator.SetTrigger("Attack");
    }

    public void ExecuteAttack()
    {
        _towerAttack.Attack(_targets);
    }

    public void EndAttack()
    {
        _attackTimer = _stats.AttackCooldown / _attackSpeed;
        _isAttacking = false;
    }

    public Transform GetFirePoint(EnemyController target)
    {
        Vector3 direction = target.transform.position - transform.position;
        return _spriteRenderer.flipX ? _leftFirePoint : _rightFirePoint;
    }

    private void Update()
    {
        UpdateTarget();
        UpdateAttackCooldown();

        if (_targets.Count > 0)
        {
            LookAtTarget();

            if(_attackTimer <= 0.0f && !_isAttacking)
            {
                Attack();
            }
        }
    }

}
