using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObject/Patterns/SpawnEnemies")]
public class SpawnEnemiesPattern : EnemyPattern
{
    public List<EnemyData> enemies;
    public int enemyPoints;
    public bool useNormalSpawnPoints;
    private List<GameObject> spawnPoints;

    public override void StartPattern(EnemyPatternController enemyPatternController, EnemyController enemyController)
    {
        if (useNormalSpawnPoints) spawnPoints = WaveManager.Instance.spawnPoints;
        else spawnPoints = enemyController.spawnPoints;
        WaveManager.Instance.StartCoroutine(WaveManager.Instance.SpawningEnemies(enemyPoints, enemies, 1, false, spawnPoints, false));
    }

    public override void UpdatePattern(EnemyPatternController enemy, EnemyController enemyController, float time, float baseTimer)
    {

    }

    public override bool IsFinished(EnemyController enemyController)
    {
        return true;
    }
}