using UnityEngine;

public class TowerAnimationEvent : MonoBehaviour
{
    private TowerController _towerController;

    private void Awake()
    {
        _towerController = GetComponentInParent<TowerController>();
    }

    public void ExecuteAttack()
    {
        _towerController.ExecuteAttack();
    }

    public void EndAttack()
    {
        _towerController.EndAttack();
    }

}
