using UnityEngine;

public class TowerAttackController : MonoBehaviour
{
    private ProjectileAttack _projectileAttack;

    public void Init()
    {
        _projectileAttack = GetComponent<ProjectileAttack>();
    }

    public void Attack(EnemyController target)
    {
        _projectileAttack.Attack(target);
    }

}
