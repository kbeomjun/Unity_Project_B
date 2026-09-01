using System.Collections.Generic;
using UnityEngine;

public class TowerStats
{
    public int Cost { get; private set; }
    public float Range { get; private set; }
    public float Damage { get; private set; }
    public float AttackCooldown { get; private set; }
    public TargetingType TargetingType { get; private set; }
    public TargetCountType TargetCountType { get; private set; }
    public int TargetCount { get; private set; }
    public List<AttackEffect> Effects { get; private set; }

    public TowerStats(TowerData data)
    {
        Cost = data.Cost;
        Range = data.Range;
        Damage = data.Damage;
        AttackCooldown = data.AttackCooldown;
        TargetingType = data.TargetingType;
        TargetCountType = data.TargetCountType;
        TargetCount = data.TargetCount;
        Effects = new List<AttackEffect>();
        foreach (AttackEffect effect in data.Effects)
        {
            if (effect == null) continue;

            AttackEffect runtimeEffect = Object.Instantiate(effect);
            Effects.Add(runtimeEffect);
        }
    }
}
