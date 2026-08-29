using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private string _towerName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private GameObject _previewPrefab;
    [SerializeField] private float _range;
    [SerializeField] private int _cost;
    [SerializeField] private float _attackCooldown;

    public string TowerName => _towerName;
    public Sprite Icon => _icon;
    public GameObject TowerPrefab => _towerPrefab;
    public GameObject PreviewPrefab => _previewPrefab;
    public float Range => _range;
    public int Cost => _cost;
    public float AttackCooldown => _attackCooldown;
}