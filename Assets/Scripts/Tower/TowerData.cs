using UnityEngine;

public enum TargetingType
{
    First,
    Last,
    Closest,
}

public enum TargetCountType
{
    Single,
    Multiple,
    All,
}

public enum TowerAttackType
{
    Projectile,
    Instant,
    Area,
}

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private string _towerName;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private GameObject _previewPrefab;
    [SerializeField] private int _cost;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private TargetingType _targetingType;
    [SerializeField] private TargetCountType _targetCountType;
    [SerializeField] private int _targetCount;
    [SerializeField] private TowerAttackType _attackType;
    [SerializeField] private ProjectileController _projectilePrefab;
    [SerializeField] private float _knockback;

    public string TowerName => _towerName;
    public Sprite Icon => _icon;
    public GameObject TowerPrefab => _towerPrefab;
    public GameObject PreviewPrefab => _previewPrefab;
    public int Cost => _cost;
    public float Range => _range;
    public float Damage => _damage;
    public float AttackCooldown => _attackCooldown;
    public TargetingType TargetingType => _targetingType;
    public TargetCountType TargetCountType => _targetCountType;
    public int TargetCount => _targetCount;
    public TowerAttackType AttackType => _attackType;
    public ProjectileController ProjectilePrefab => _projectilePrefab;
    public float Knockback => _knockback;
}