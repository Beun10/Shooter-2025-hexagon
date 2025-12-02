using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health { private get; set; }
    private float maxHealth;
    private LevelManager levelManager;
    [SerializeField] private GameObject sprite;
    private Color baseColor;
    private float timer;
    [SerializeField] private float whiteDuration;
    [SerializeField] private EnemyHealth parent;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float damageParticleBurst;
    private Gradient gradient = new Gradient();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth.Instance = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void Initialize(int health, Color baseColor, Material particleMaterial)
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        this.health = health * levelManager.enemyHealthBuff;
        maxHealth = this.health;
        this.baseColor = baseColor;
        particle.startColor = baseColor;
        ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
        renderer.material = particleMaterial;
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = baseColor;
        colorKeys[0].time = 0f;
        colorKeys[1].color = baseColor;
        colorKeys[1].time = 1f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = 0f;
        alphaKeys[1].time = 1f;
        gradient.SetKeys(colorKeys, alphaKeys);
        ParticleSystem.ColorOverLifetimeModule particleColor = particle.colorOverLifetime;
        particleColor.color = gradient;
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
        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        burst.count = damage * damageParticleBurst;
        particle.emission.SetBurst(0, burst);
        particle.Play();
        timer = 0;
        if (health < 0)
        {
            levelManager.enemies.Remove(this.gameObject);
            if (levelManager.enemies.Count <= 0) levelManager.LevelCompleted();
            Destroy(gameObject);
        }
    }
}
