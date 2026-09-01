using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Tower/AttackEffect/Damage")]
public class DamageEffect : AttackEffect
{
    public override void Apply(TowerController attacker, EnemyController target)
    {
        target.TakeDamage(attacker.Stats.Damage);
    }
}
