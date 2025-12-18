using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObject/Patterns/SpawnShield")]
public class SpawnShieldPattern: EnemyPattern
{
    public GameObject shieldPrefab;
    public EnemyData shieldData;

    public override void StartPattern(EnemyPatternController enemyPatternController, EnemyController enemyController)
    {
        GameObject shield = Instantiate(shieldPrefab, Shooting.Instance.gameObject.transform.position, Quaternion.identity);
        shield.GetComponent<EnemyController>().spawnPoint = Shooting.Instance.gameObject.transform.position;
        shield.GetComponent<EnemyController>().Initialize(shieldData);
        shield.GetComponent<EnemyController>().Spawning();
    }

    public override void UpdatePattern(EnemyPatternController enemy, EnemyController enemyController, float time, float baseTimer)
    {

    }

    public override bool IsFinished(EnemyController enemyController)
    {
        return true;
    }
}