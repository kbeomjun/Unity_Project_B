using UnityEngine;

[CreateAssetMenu(fileName = "SlowEffect", menuName = "Tower/AttackEffect/Slow")]
public class SlowEffect : AttackEffect
{
    [SerializeField] private float _percent;
    [SerializeField] private float _duration;

    public float Percent => _percent;
    public float Duration => _duration;

    public void SetSlowPercent(float value)
    {
        _percent = value;
    }

    public void SetDuration(float value)
    {
        _duration = value;
    }

    public override void Apply(TowerController tower, EnemyController target)
    {
        SlowStatusEffect statusEffect = new SlowStatusEffect(_percent, _duration);
        target.StatusEffectController.ApplyStatusEffect(statusEffect);
    }
}

