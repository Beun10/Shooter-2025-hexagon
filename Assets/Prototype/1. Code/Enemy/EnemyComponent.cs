using NUnit.Framework;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private EnemyComponentData data;
    private float damage;
    private float attackCooldown;
    private float attackTimer;
    private GameObject projectile;
    private float projectileSpeed;
    private Vector3 projectileSize;
    private float projectileDuration;
    private bool spawned = false;
    private float heal;
    private float healingSpeed;
    private EnemyController enemyController;
    private float decayRate;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer healthSpriteRenderer;
    [SerializeField] private PolygonCollider2D hitbox;

    private void Initialize()
    {
        damage = data.damage * LevelManager.Instance.enemyDamageBuff;
        attackCooldown = data.attackCooldown;
        if (health != null) health.health = data.health;
        if (spriteRenderer != null) spriteRenderer.sprite = data.sprite;
        if (data.color != null) healthSpriteRenderer.color = data.color;
        if (data.sprite != null) healthSpriteRenderer.sprite = data.sprite;
        //transform.localScale = data.size;
        projectile = data.projectile;
        projectileSpeed = data.projectileSpeed;
        projectileSize = data.projectileSize;
        projectileDuration = data.projectileDuration;
        if (health != null) health.Initialize(data.health, data.color, data.particleMaterial, data.isCore, data.primaryDamageMultiplier, data.secondaryDamageMultiplier);
        this.healingSpeed = data.healingSpeed * LevelManager.Instance.enemyHealthBuff;
        this.decayRate = data.decayRate * LevelManager.Instance.enemyHealthBuff;
        spawned = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            if ((attackTimer < attackCooldown) && enemyController.cannotAttackDuration <= 0) attackTimer += Time.deltaTime;
            if (projectile != null && attackTimer >= attackCooldown && spawned)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<EnemyProjectileController>().Initialize(projectileSpeed, projectileDuration, damage, projectileSize);
                attackTimer = 0;
            }
            if (heal > 0)
            {
                if(healingSpeed * Time.deltaTime < heal) health.Heal(healingSpeed * Time.deltaTime);
                else health.Heal(heal);
                heal -= healingSpeed * Time.deltaTime;
                if (heal < 0) heal = 0;
            }
            if (decayRate > 0) health.TakeDamage(decayRate * Time.deltaTime, 0, GameManager.DamageType.Other);
        }
    }
    public void Spawning(EnemyComponentData data)
    {
        if (this.data == null) this.data = data;
        spawned = true;
        Initialize();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackTimer >= attackCooldown && spawned)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            attackTimer = 0;
        }
        if (collision.gameObject.CompareTag("PassiveDamage") && collision.GetComponent<PassiveDamage>().damage > 0)
        {
            health.TakeDamage(collision.GetComponent<PassiveDamage>().damage * Time.deltaTime, collision.GetComponent<PassiveDamage>().lifesteal, GameManager.DamageType.Other);
        }
    }
    public void Heal(float heal, float percentHeal)
    {
        this.heal += heal;
        this.heal += health.maxHealth * percentHeal;
    }
}
