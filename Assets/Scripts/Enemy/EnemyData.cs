using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _speed;

    public string Name => _name;
    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
}
