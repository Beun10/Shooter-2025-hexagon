using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health { private get; set; }
    private int maxHealth;
    private LevelManager levelManager;
    [SerializeField] private GameObject sprite;
    private Color baseColor;
    private float timer;
    [SerializeField] private float whiteDuration;
    [SerializeField] private EnemyHealth parent;
    [SerializeField] private ParticleSystem particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth.Instance = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void Initialize(int health, Color baseColor)
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        this.health = health * levelManager.enemyHealthBuff;
        maxHealth = health;
        this.baseColor = baseColor;
        particle.startColor = baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < whiteDuration)
        {
            timer += Time.deltaTime;
            if (timer >= whiteDuration) sprite.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }
    public void TakeDamage(float damage, float lifesteal)
    {
        if (damage > health) PlayerHealth.Instance.TakeDamage(-(health * lifesteal));
        else PlayerHealth.Instance.TakeDamage(-(damage * lifesteal));
        health -= damage;
        sprite.transform.localScale = new Vector3(health / maxHealth , health / maxHealth, 1);
        sprite.GetComponent<SpriteRenderer>().color = Color.white;
        this.gameObject.GetComponent<ParticleSystem>().Play();
        timer = 0;
        if (health < 0)
        {
            levelManager.enemies.Remove(this.gameObject);
            if (levelManager.enemies.Count <= 0) levelManager.LevelCompleted();
            Destroy(gameObject);
        }
    }
}
