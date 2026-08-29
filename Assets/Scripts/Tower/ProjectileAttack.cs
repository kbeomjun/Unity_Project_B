using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private Transform _RightFirePoint;
    [SerializeField] private Transform _LeftFirePoint;

    public void Attack(EnemyController target)
    {
        if (target == null) return;

        ProjectileController projectile = null;
        if (!_spriteRenderer.flipX)
        {
            projectile = Instantiate(_projectilePrefab, _RightFirePoint.position, _RightFirePoint.rotation);
        }
        else
        {
            projectile = Instantiate(_projectilePrefab, _LeftFirePoint.position, _LeftFirePoint.rotation);
        }
        projectile.Init(target);
    }

}
