using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField] private EnemyComponentData data;
    private float damage;
    private float attackCooldown;
    private GameObject player;
    private float timer;
    private GameObject projectile;
    private float projectileSpeed;
    private Vector3 projectileSize;
    private float projectileDuration;
    private bool spawned;
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
        transform.localScale = data.size;
        projectile = data.projectile;
        projectileSpeed = data.projectileSpeed;
        projectileSize = data.projectileSize;
        projectileDuration = data.projectileDuration;
        if (health != null) health.Initialize(data.health, data.color, data.particleMaterial, data.isCore);
        spawned = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            if (timer < attackCooldown) timer += Time.deltaTime;
            if (projectile != null && timer >= attackCooldown && spawned)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<EnemyProjectileController>().Initialize(projectileSpeed, projectileDuration, damage, projectileSize);
                timer = 0;
            }
        }
    }
    public void Spawning()
    {
        spawned = true;
        Initialize();
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
