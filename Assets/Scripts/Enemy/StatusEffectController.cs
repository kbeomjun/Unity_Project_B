using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    private EnemyController _enemy;
    private List<StatusEffect> _activeEffects;

    public void Init(EnemyController enemy)
    {
        _enemy = enemy;
        _activeEffects = new List<StatusEffect>();
    }

    public void ApplyStatusEffect(StatusEffect newEffect)
    {
        if (newEffect == null) return;

        foreach (StatusEffect effect in _activeEffects)
        {
            if (!effect.CanCombine(newEffect)) continue;

            effect.Combine(_enemy, newEffect);
            return;
        }

        _activeEffects.Add(newEffect);
        newEffect.Apply(_enemy);
    }

    private void Update()
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            StatusEffect effect = _activeEffects[i];
            effect.Update(_enemy, Time.deltaTime);

            if (effect.IsFinished)
            {
                effect.Remove(_enemy);
                _activeEffects.RemoveAt(i);
            }
        }
    }

}
