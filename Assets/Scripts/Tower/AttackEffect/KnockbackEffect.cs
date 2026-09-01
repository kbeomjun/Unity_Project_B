using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackEffect", menuName = "Tower/AttackEffect/Knockback")]
public class KnockbackEffect : AttackEffect
{
    public override void Apply(TowerController attacker, EnemyController target)
    {
        target.ApplyKnockback(attacker.Stats.Knockback);
    }
}
