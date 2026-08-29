using UnityEngine;

public class TowerAnimationEvent : MonoBehaviour
{
    private TowerController _towerController;

    private void Awake()
    {
        _towerController = GetComponentInParent<TowerController>();
    }

    public void FireProjectile()
    {
        _towerController.FireProjectile();
    }

    public void EndAttack()
    {
        _towerController.EndAttack();
    }

}
