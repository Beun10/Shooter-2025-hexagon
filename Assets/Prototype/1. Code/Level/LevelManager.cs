using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelState currentState = LevelState.Start;
    public static LevelManager Instance;
    [SerializeField] private float enemyPointsLevelMultiplier;
    [SerializeField] private int baseEnemyPoints;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private TextMeshPro levelText;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private BuffManager buffManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 mapExpansion;
    [SerializeField] private GameObject Walls;
    [SerializeField] private int moreWavesInterval;
    [SerializeField] private float levelEnemyHealthBuff;
    [SerializeField] private float levelEnemyDamageBuff;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float outLevelCameraSize;
    [SerializeField] private float inLevelCameraSize;
    [SerializeField] private float levelBuffPointsMultiplier;
    public float enemyHealthBuff = 1;
    public float enemyDamageBuff = 1;
    public int bossWavesInterval;
    public List<GameObject> enemies = new List<GameObject>();
    private float enemyPoints;
    public int currentLevel;
    public int remainingEnemies;
    public bool levelIsOver;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        enemyPoints = baseEnemyPoints;
        currentLevel = 0;
        levelIsOver = true;
        playerCamera.orthographicSize = outLevelCameraSize;
    }
    public void NextLevel()
    {
        waveManager.lastWave = false;
        buffManager.ToggleButtons(false);
        levelIsOver = false;
        currentLevel += 1;
        if (currentLevel % moreWavesInterval == 0) waveManager.waveAmount += 1;
        levelText.text = "Level " + currentLevel;
        enemyPoints *= enemyPointsLevelMultiplier;
        enemyPoints = Mathf.RoundToInt(enemyPoints);
        Walls.transform.localScale += mapExpansion;
        waveManager.SpawningCoroutine = StartCoroutine(waveManager.SpawningEnemies(enemyPoints));
        playerCamera.orthographicSize = inLevelCameraSize;
    }

    public void LevelCompleted()
    {
        levelIsOver = true;
        nextLevelButton.GetComponent<SpriteRenderer>().enabled = true;
        buffManager.buffPoints += buffManager.levelBuffPoints + Mathf.Ceil(buffManager.levelBuffPoints * (levelBuffPointsMultiplier * currentLevel));
        buffManager.UpdatePoints();
        buffManager.ToggleButtons(true);
        player.GetComponent<PlayerHealth>().TakeDamage(-9999);
        enemyHealthBuff += levelEnemyHealthBuff;
        enemyDamageBuff += levelEnemyDamageBuff;
        playerCamera.orthographicSize = outLevelCameraSize;
    }

    public enum LevelState
    {
        Start,
        Playing,
        End,
        Restart
    }
}
