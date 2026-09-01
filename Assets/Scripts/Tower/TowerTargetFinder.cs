using System.Collections.Generic;
using UnityEngine;

public class TowerTargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;

    private TowerStats _stats;

    public void Init(TowerStats stats)
    {
        _stats = stats;
    }

    public List<EnemyController> FindTargets()
    {
        List<EnemyController> enemies = GetEnemiesInRange();
        SortTargets(enemies, _stats.TargetingType);
        return SelectTargets(enemies, _stats.TargetCountType, _stats.TargetCount);
    }

    private List<EnemyController> GetEnemiesInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _stats.Range, _enemyLayer);

        List<EnemyController> enemies = new();

        foreach (Collider2D collider in colliders)
        {
            EnemyController enemy = collider.GetComponentInParent<EnemyController>();

            if (enemy == null) continue;
            if (enemy.State == EnemyState.Dead) continue;
            if (enemies.Contains(enemy)) continue;

            enemies.Add(enemy);
        }

        return enemies;
    }

    private void SortTargets(List<EnemyController> enemies, TargetingType targetingType)
    {
        switch (targetingType)
        {
            case TargetingType.First:
                enemies.Sort((a, b) => b.Progress.CompareTo(a.Progress));
                break;

            case TargetingType.Last:
                enemies.Sort((a, b) => a.Progress.CompareTo(b.Progress));
                break;

            case TargetingType.Closest:
                enemies.Sort((a, b) => Vector2.Distance(transform.position, a.transform.position)
                                        .CompareTo(Vector2.Distance(transform.position, b.transform.position)));
                break;
        }
    }

    private List<EnemyController> SelectTargets(List<EnemyController> enemies, TargetCountType targetCountType, int targetCount)
    {
        if (enemies.Count == 0) return new List<EnemyController>();

        switch (targetCountType)
        {
            case TargetCountType.Single:
                return new List<EnemyController>
                {
                    enemies[0]
                };

            case TargetCountType.Multiple:
                int count = Mathf.Min(targetCount, enemies.Count);
                return enemies.GetRange(0, count);

            case TargetCountType.All:
                return enemies;

            default:
                return new List<EnemyController>();
        }
    }

}
