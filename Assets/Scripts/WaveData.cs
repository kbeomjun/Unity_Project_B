using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Stage/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemySpawnData> enemySpawnDatas;
}
