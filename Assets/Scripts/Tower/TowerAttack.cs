using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private TowerController _tower;
    private TowerAttackType _attackType;
    private ProjectileController _projectilePrefab;

    public void Init(TowerController tower)
    {
        _tower = tower;
        _attackType = tower.Data.AttackType;
        _projectilePrefab = tower.Data.ProjectilePrefab;
    }

    public void Attack(List<EnemyController> targets)
    {
        if (targets == null || targets.Count == 0) return;

        switch (_attackType)
        {
            case TowerAttackType.Projectile:
                AttackProjectile(targets);
                break;
            
            case TowerAttackType.Instant:
                AttackInstant(targets);
                break;

            case TowerAttackType.Area:
                AttackArea(targets);
                break;
        }
    }

    private void AttackInstant(List<EnemyController> targets)
    {
        foreach (EnemyController target in targets)
        {
            if (target == null) continue;

            ApplyEffects(target);
        }
    }

    private void AttackProjectile(List<EnemyController> targets)
    {
        foreach (EnemyController target in targets)
        {
            if (target == null) continue;

            Transform firePoint = _tower.GetFirePoint(target);
            ProjectileController projectile = Instantiate(_projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.Init(_tower, target, _tower.Stats.Effects);
        }
    }

    private void AttackArea(List<EnemyController> targets)
    {
        foreach (EnemyController target in targets)
        {
            if (target == null) continue;

            ApplyEffects(target);
        }
    }

    private void ApplyEffects(EnemyController target)
    {
        foreach (AttackEffect effect in _tower.Stats.Effects)
        {
            if (effect == null) continue;

            effect.Apply(_tower, target);
        }
    }

}
