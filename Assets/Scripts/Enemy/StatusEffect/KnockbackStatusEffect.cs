using UnityEngine;

public class KnockbackStatusEffect : StatusEffect
{
    public float Distance { get; private set; }
    public float RemainingDistance { get; private set; }
    public float Speed { get; private set; }

    public override bool IsFinished => RemainingDistance <= 0.0f;

    public KnockbackStatusEffect(float distance, float duration)
    {
        Distance = distance;
        RemainingDistance = distance;
        Speed = distance / duration;
    }

    public override void Apply(EnemyController enemy)
    {
    }

    public override void Update(EnemyController enemy, float deltaTime)
    {
        float distance = Speed * deltaTime;

        if (distance > RemainingDistance)
        {
            distance = RemainingDistance;
        }

        enemy.MoveBackward(distance);
        RemainingDistance -= distance;
    }

    public override void Remove(EnemyController enemy)
    {
    }

    public override bool CanCombine(StatusEffect other)
    {
        return other is KnockbackStatusEffect;
    }

    public override void Combine(EnemyController enemy, StatusEffect other)
    {
        KnockbackStatusEffect knockback = (KnockbackStatusEffect)other;

        if (knockback.Distance <= Distance) return;

        Distance = knockback.Distance;
        RemainingDistance = knockback.Distance;
        Speed = knockback.Speed;
    }
}
