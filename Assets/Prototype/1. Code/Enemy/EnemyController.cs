using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private EnemyComponentData armorData;
    private EnemyComponentData coreData;
    private float movementSpeed;
    private int spawnChance;
    private bool spawned;
    public Vector2 spawnPoint = new Vector2(500, 500);
    private int componentAmount;
    private float rotationSpeed;
    private float minimalDistance;
    private float playerDistance;
    private float rootedDuration;
    public float cannotAttackDuration {  get; private set; }
    public float retreatDuration { get; private set; }
    public List<GameObject> spawnPoints;
    [SerializeField] private EnemyComponent core;
    [SerializeField] private List<EnemyComponent> armor;

    public void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        armorData = data.armor;
        coreData = data.core;
        movementSpeed = data.speed;
        spawnChance = data.spawnChance;
        transform.localScale = data.size;
        componentAmount = armor.Count;
        rotationSpeed = data.rotatingSpeed;
        minimalDistance = data.minimalDistance;
        GetComponent<EnemyPatternController>().enabled = false;
        GetComponent<EnemyPatternController>().Initialize(data.patterns, data.firstPatternDelay, data.patternCooldown, data.patternCooldownVariation);
    }
    void Start()
    {
        if (spawnPoint == new Vector2(500, 500))
        {
            spawnPoint = transform.position;
            transform.position = new Vector3(500, 500, 500);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            playerDistance = Mathf.Abs(transform.position.x - Shooting.Instance.transform.position.x) + Mathf.Abs(transform.position.y - Shooting.Instance.transform.position.y);
            if (rootedDuration <= 0)
            {
                if (playerDistance < minimalDistance || retreatDuration > 0) transform.position = Vector2.MoveTowards(transform.position, Shooting.Instance.transform.position, -movementSpeed * Time.deltaTime);
                else transform.position = Vector2.MoveTowards(transform.position, Shooting.Instance.transform.position, movementSpeed * Time.deltaTime);
            }
            GameObject target = Shooting.Instance.gameObject;
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion quaternion = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, rotationSpeed * Time.deltaTime);
            Timers();
        }
    }

    public void Spawning()
    {
        if (!spawned)
            if ((Random.Range(1, 101) <= spawnChance) || WaveManager.Instance.lastWave)
            {
                spawned = true;
                GetComponent<EnemyPatternController>().enabled = true;
                transform.position = spawnPoint;
                core.Spawning(coreData);
                foreach (EnemyComponent component in armor)
                {
                    component.Spawning(armorData);
                }
            }
    }

    public void ComponentDestroyed(EnemyComponent component, bool isCore)
    {
        componentAmount -= 1;
        armor.Remove(component);
        if (isCore)
        {
            if(LevelManager.Instance.enemies.Contains(gameObject)) LevelManager.Instance.enemies.Remove(this.gameObject);
            if (LevelManager.Instance.enemies.Count <= 0 && !LevelManager.Instance.levelIsOver) LevelManager.Instance.LevelCompleted();
            Destroy(gameObject);
        }
    }
    private void Timers()
    {
        if (rootedDuration > 0) rootedDuration -= Time.deltaTime;
        else rootedDuration = 0;
        if (retreatDuration > 0) retreatDuration -= Time.deltaTime;
        else retreatDuration = 0;
        if (cannotAttackDuration < 0) cannotAttackDuration -= Time.deltaTime;
        else cannotAttackDuration = 0;
    }
    public void ChangeVariables(float retreat = 0, float rooted = 0, float cannotAttack = 0)
    {
        retreatDuration += retreat;
        rootedDuration += rooted;
        cannotAttackDuration += cannotAttack;
    }
    public void HealComponents(float heal, float percentHeal)
    {
        core.Heal(heal, percentHeal);
        foreach (EnemyComponent component in armor) component.Heal(heal, percentHeal);
    }
}
