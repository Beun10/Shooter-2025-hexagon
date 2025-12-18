using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "ScriptableObject/Patterns/Retreat")]
public class EnemyRetreatPattern : EnemyPattern
{
    public float retreatDuration;
    public float rootedDuration;
    public float healing;

    public override void StartPattern(EnemyPatternController enemyPatternController, EnemyController enemyController)
    {
        enemyController.ChangeVariables(retreat: retreatDuration);
    }

    public override void UpdatePattern(EnemyPatternController enemy, EnemyController enemyController, float time, float baseTimer)
    {
        if (enemyController.retreatDuration <= 0)
        {
            enemyController.ChangeVariables(rooted: rootedDuration, cannotAttack: rootedDuration);
            enemyController.HealComponents(0, healing);
        }
    }

    public override bool IsFinished(EnemyController enemyController)
    {
        return enemyController.retreatDuration <= 0;
    }
}