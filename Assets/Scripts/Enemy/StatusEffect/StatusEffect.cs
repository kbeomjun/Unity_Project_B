using System;

public abstract class StatusEffect
{
    public abstract bool IsFinished { get; }
    public abstract void Apply(EnemyController enemy);
    public abstract void Update(EnemyController enemy, float deltaTime);
    public abstract void Remove(EnemyController enemy);
    public abstract bool CanCombine(StatusEffect other);
    public abstract void Combine(EnemyController enemy, StatusEffect other);
}
