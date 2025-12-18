using UnityEngine;

public abstract class EnemyPattern : ScriptableObject
{
    public string patternName;

    // Appelé quand le pattern commence
    public abstract void StartPattern(EnemyPatternController enemy, EnemyController enemyController);

    // Appelé à chaque update du boss
    public abstract void UpdatePattern(EnemyPatternController enemy, EnemyController enemyController, float time, float baseTimer);

    // True si le pattern est terminé
    public abstract bool IsFinished(EnemyController enemyController);
}
