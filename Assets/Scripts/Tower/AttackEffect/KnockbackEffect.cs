using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackEffect", menuName = "Tower/AttackEffect/Knockback")]
public class KnockbackEffect : AttackEffect
{
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;

    public float Distance => _distance;
    public float Duration => _duration;

    public void SetDistance(float value)
    {
        _distance = value;
    }

    public void SetDuration(float value)
    {
        _duration = value;
    }

    public override void Apply(TowerController attacker, EnemyController target)
    {
        KnockbackStatusEffect statusEffect = new KnockbackStatusEffect(_distance, 0.2f);
        target.StatusEffectController.ApplyStatusEffect(statusEffect);
    }
}
