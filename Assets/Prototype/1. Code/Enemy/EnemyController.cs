using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    private float damage;
    private float attackCooldown;
    private float movementSpeed;
    private GameObject prefab;
    private GameObject player;
    private float timer;
    private int spawnChance;
    private GameObject projectile;
    private float projectileSpeed;
    private Vector3 projectileSize;
    private float projectileDuration;
    public bool spawned;
    private Vector2 spawnPoint;
    private bool component;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private BoxCollider2D hitbox;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer healthSpriteRenderer;

    public void Initialize(EnemyData enemyData)
    {
        this.data = enemyData;
        damage = data.damage * LevelManager.Instance.enemyDamageBuff;
        attackCooldown = data.attackCooldown;
        movementSpeed = data.speed;
        spawnChance = data.spawnChance;
        if(health != null) health.health = data.health;
        component = data.component;
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = data.sprite;
        }
        healthSpriteRenderer.color = data.color;
        healthSpriteRenderer.sprite = data.sprite;
        transform.localScale = data.size;
        if(hitbox != null) hitbox.size = data.hitbox;
        projectile = data.projectile;
        projectileSpeed = data.projectileSpeed;
        projectileSize = data.projectileSize;
        projectileDuration = data.projectileDuration;
        if(health != null) health.Initialize(data.health, data.color, data.particleMaterial);
    }
    void Start()
    {
        if (!data.component)
        {
            spawnPoint = transform.position;
            transform.position = new Vector3(500, 500, 500);
        }
        player = GameObject.Find("Player");
        if (data.component) Initialize(data);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned) transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        if (timer < attackCooldown) timer += Time.deltaTime;
        if (projectile != null && timer >= attackCooldown && spawned)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<EnemyProjectileController>().Initialize(projectileSpeed, projectileDuration, damage, projectileSize);
            timer = 0;
        }
    }

    public void Spawning()
    {
        if (!spawned)
            if ((Random.Range(1, 101) <= spawnChance) || WaveManager.Instance.lastWave)
            {
                spawned = true;
                transform.position = spawnPoint;
            }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && timer >= attackCooldown && spawned)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            timer = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PassiveDamage") && collision.GetComponent<PassiveDamage>().damage > 0)
        {
            health.TakeDamage(collision.GetComponent<PassiveDamage>().damage * Time.deltaTime, collision.GetComponent<PassiveDamage>().lifesteal);
        }
    }
}
