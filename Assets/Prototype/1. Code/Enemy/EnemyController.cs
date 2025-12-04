using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private float movementSpeed;
    private GameObject player;
    private int spawnChance;
    private bool spawned;
    private Vector2 spawnPoint;
    private int componentAmount;
    [SerializeField] private List<EnemyComponent> components;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        movementSpeed = data.speed;
        spawnChance = data.spawnChance;
        transform.localScale = data.size;
        componentAmount = components.Count;
    }
    void Start()
    {
        spawnPoint = transform.position;
        transform.position = new Vector3(500, 500, 500);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned) transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    public void Spawning()
    {
        if (!spawned)
            if ((Random.Range(1, 101) <= spawnChance) || WaveManager.Instance.lastWave)
            {
                spawned = true;
                transform.position = spawnPoint;
                foreach (EnemyComponent component in components)
                {
                    component.Spawning();
                }
            }
    }

    public void ComponentDestroyed(EnemyComponent component, bool isCore)
    {
        componentAmount -= 1;
        components.Remove(component);
        if ((componentAmount < 1) || isCore)
        {
            LevelManager.Instance.enemies.Remove(this.gameObject);
            if (LevelManager.Instance.enemies.Count <= 0 && !LevelManager.Instance.levelIsOver) LevelManager.Instance.LevelCompleted();
            Destroy(gameObject);
        }
    }
    
}
