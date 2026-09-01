using UnityEngine;

public class SlowStatusEffect : StatusEffect
{
    public float Percent { get; private set; }
    public float RemainingDuration { get; private set; }

    public override bool IsFinished => RemainingDuration <= 0.0f;

    public SlowStatusEffect(float percent, float duration)
    {
        Percent = percent;
        RemainingDuration = duration;
    }

    public override void Apply(EnemyController enemy)
    {
        enemy.SetSpeedMultiplier(1.0f - Percent);
        enemy.PlaySlowEffect();
    }

    public override void Update(EnemyController enemy, float deltaTime)
    {
        RemainingDuration -= deltaTime;
    }

    public override void Remove(EnemyController enemy)
    {
        enemy.SetSpeedMultiplier(1.0f);
        enemy.StopSlowEffect();
    }

    public override bool CanCombine(StatusEffect other)
    {
        return other is SlowStatusEffect;
    }

    public override void Combine(EnemyController enemy, StatusEffect other)
    {
        SlowStatusEffect slow = (SlowStatusEffect)other;
        if (slow.Percent > Percent)
        {
            Percent = slow.Percent;
            RemainingDuration += slow.RemainingDuration;
            enemy.SetSpeedMultiplier(1.0f - Percent);
        }
        else
        {
            RemainingDuration += slow.RemainingDuration;
        }
    }
}
