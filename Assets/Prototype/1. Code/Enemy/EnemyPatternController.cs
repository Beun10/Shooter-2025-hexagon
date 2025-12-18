using UnityEngine;

public class EnemyPatternController : MonoBehaviour
{
    private EnemyPattern[] patterns;
    private float firstPatternDelay;
    private float patternCooldown;
    private Vector2 patternDelayVariation;
    private float basePatternCooldown;
    private int currentPatternIndex = 0;
    private bool firstPattern = true;
    private float timer;
    private float baseTimer;
    private EnemyPattern currentPattern;

    private void Start()
    {
        firstPatternDelay += Random.Range(patternDelayVariation.x, patternDelayVariation.y);
        basePatternCooldown = patternCooldown;
    }
    public void Initialize(EnemyPattern[] patterns, float firstPatternDelay, float patternCooldown, Vector2 patternDelayVariation)
    {
        this.patterns = patterns;
        this.firstPatternDelay = firstPatternDelay;
        this.patternCooldown = patternCooldown;
        this.patternDelayVariation = patternDelayVariation;
    }
    void FirstPattern()
    {
        if (patterns.Length > 0)
        {
            currentPattern = patterns[currentPatternIndex];
            currentPattern.StartPattern(this, GetComponent<EnemyController>());
            baseTimer = timer;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > firstPatternDelay && firstPattern)
        {
            firstPattern = false;
            FirstPattern();
        }
        if (currentPattern != null)
        {
            currentPattern.UpdatePattern(this, GetComponent<EnemyController>(), timer, baseTimer);
            if (currentPattern.IsFinished(GetComponent<EnemyController>()))
            {
                timer = 0;
                currentPattern = null;
            }
        }
        if (!firstPattern && currentPattern == null && timer > patternCooldown)
        {
            timer = 0;
            NextPattern();
            patternCooldown = basePatternCooldown + Random.Range(patternDelayVariation.x, patternDelayVariation.y);
        }
    }

    void NextPattern()
    {
        if (patterns.Length > 1)
        {
            currentPatternIndex = Random.Range(0, patterns.Length);
            currentPattern = patterns[currentPatternIndex];
            currentPattern.StartPattern(this, GetComponent<EnemyController>());
        }
        else if (patterns.Length == 1)
        {
            currentPattern = patterns[0];
            currentPattern.StartPattern(this, GetComponent<EnemyController>());
        }
    }
}