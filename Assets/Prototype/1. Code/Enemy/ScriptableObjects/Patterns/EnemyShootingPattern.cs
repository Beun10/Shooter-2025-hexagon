/*
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Patterns/Shoot")]
public class EnemyShootingPattern : EnemyPattern
{
    public float duration = 2f;
    private float timer;

    public override void StartPattern(EnemyPatternController enemy, EnemyController enemyController)
    {
        timer = 0f;
        // enemy.Shoot();  une fois au début
    }

    public override void UpdatePattern(EnemyPatternController enemy)
    {
        timer += Time.deltaTime;

        // Tir répété
        if (timer % 0.5f < Time.deltaTime)
        {
            //enemy.Shoot();
        }
    }

    public override bool IsFinished()
    {
        return timer >= duration;
    }
}
*/