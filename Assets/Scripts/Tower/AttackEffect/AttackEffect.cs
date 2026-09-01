using UnityEngine;

public abstract class AttackEffect : ScriptableObject
{
    public abstract void Apply(TowerController attacker, EnemyController target);
}
