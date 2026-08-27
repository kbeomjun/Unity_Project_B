using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private TowerData _towerData;

    public void OnClick()
    {
        TowerManager.Instance.SelectTower(_towerData);
    }

}
