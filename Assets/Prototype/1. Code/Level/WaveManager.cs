using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int waveTimer;
    public int waveAmount;
    [SerializeField] private List<EnemyData> enemiesTypes = new List<EnemyData>();
    [SerializeField] private List<EnemyData> bosses = new List<EnemyData>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] private List<TextMeshPro> waveText;
    public Coroutine SpawningCoroutine;
    public bool lastWave;
    private int enemiesTotalSpawnWeight;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        foreach (EnemyData enemy in enemiesTypes)
        {
            enemiesTotalSpawnWeight += enemy.spawnWeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SpawningEnemies(float totalPoints)
    {
        EnemiesInitialisation(((int)totalPoints));
        int waveTextNumber = waveAmount;
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < waveAmount; i++)
        {
            if (i+1 == waveAmount) lastWave = true;
            foreach (GameObject enemy in levelManager.enemies)
            {
                enemy.GetComponent<EnemyController>().Spawning();
            }
            waveTextNumber -= 1;
            foreach (TextMeshPro text in waveText) text.text = waveTextNumber.ToString();
            yield return new WaitForSeconds(waveTimer);
        }
        StopCoroutine(SpawningCoroutine);
        SpawningCoroutine = null;
        yield return null;
    }
    private void EnemiesInitialisation(int points)
    {
        int remainingPoints = points;
        int i = 0;
        while (remainingPoints > 0)
        {
            Vector2 selectedSpawnPoint = spawnPoints[i % spawnPoints.Count].transform.position;
            EnemyData enemyType = RandomEnemy(enemiesTypes);
            GameObject enemy = (Instantiate(enemyPrefab, selectedSpawnPoint, Quaternion.identity));
            enemy.GetComponent<EnemyController>().Initialize(enemyType);
            remainingPoints -= enemyType.cost;
            levelManager.enemies.Add(enemy);
            i++;
        }
        if (levelManager.currentLevel % levelManager.bossWavesInterval == 0)
        {
            Vector2 selectedSpawnPoint = spawnPoints[i % spawnPoints.Count].transform.position;
            EnemyData enemyType = RandomEnemy(bosses);
            GameObject enemy = Instantiate(enemyType.prefab, selectedSpawnPoint, Quaternion.identity);
            enemy.GetComponent<EnemyController>().Initialize(enemyType);
            levelManager.enemies.Add(enemy);
        }
    }
    private EnemyData RandomEnemy(List<EnemyData> enemyList)
    {
        int i = UnityEngine.Random.Range(0, enemiesTotalSpawnWeight);
        int usedWeight = 0;
        foreach (EnemyData enemy in enemyList)
        {
            if (i - usedWeight <= enemy.spawnWeight) return enemy;
            else usedWeight += enemy.spawnWeight;
        }
        return enemyList[0];
    }
}
