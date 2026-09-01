using System;

public class TowerStats
{
    public float Cost { get; private set; }
    public float Range { get; private set; }
    public float Damage { get; private set; }
    public float AttackCooldown { get; private set; }
    public TargetingType TargetingType { get; private set; }
    public TargetCountType TargetCountType { get; private set; }
    public int TargetCount { get; private set; }
    public float Knockback { get; private set; }

    public TowerStats(TowerData data)
    {
        Cost = data.Cost;
        Range = data.Range;
        Damage = data.Damage;
        AttackCooldown = data.AttackCooldown;
        TargetingType = data.TargetingType;
        TargetCountType = data.TargetCountType;
        TargetCount = data.TargetCount;
        Knockback = data.Knockback;
    }
}
