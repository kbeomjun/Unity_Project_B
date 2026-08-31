using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Stage/WaveData")]
public class WaveData : ScriptableObject
{
    [SerializeField] private List<EnemySpawnData> _enemySpawnDatas;

    public List<EnemySpawnData> EnemySpawnDatas => _enemySpawnDatas;
}
