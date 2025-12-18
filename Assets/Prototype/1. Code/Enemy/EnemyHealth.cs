using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health { private get; set; }
    public float maxHealth { get; private set; }
    [SerializeField] private GameObject sprite;
    private Color baseColor;
    private float timer;
    private bool isCore;
    private float primaryDamageMultiplier;
    private float secondaryDamageMultiplier;
    [SerializeField] private float whiteDuration;
    [SerializeField] private EnemyController parent;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float damageParticleBurst;
    private Gradient gradient = new Gradient();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealth.Instance = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public void Initialize(int health, Color baseColor, Material particleMaterial, bool isCore, float primaryDamageMultiplier, float SecondaryDamageMutliplier)
    {
        this.health = health * LevelManager.Instance.enemyHealthBuff;
        maxHealth = this.health;
        this.primaryDamageMultiplier = primaryDamageMultiplier;
        if (isCore) this.primaryDamageMultiplier *= Shooting.Instance.primaryCoreDamageBuff;
        this.secondaryDamageMultiplier = SecondaryDamageMutliplier;
        this.isCore = isCore;
        this.baseColor = baseColor;
        ParticleSystem.MainModule particleBaseColor = GetComponent<ParticleSystem>().main;
        particleBaseColor.startColor = baseColor;
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
    public void TakeDamage(float damage, float lifesteal, GameManager.DamageType damageType)
    {
        if (damageType == GameManager.DamageType.Primary) damage *= primaryDamageMultiplier;
        else if (damageType == GameManager.DamageType.Secondary) damage *= secondaryDamageMultiplier;
        if (damage > health) PlayerHealth.Instance.TakeDamage(-(health * lifesteal));
        else PlayerHealth.Instance.TakeDamage(-(damage * lifesteal));
        health -= damage;
        sprite.transform.localScale = new Vector3(health / maxHealth , health / maxHealth, 1);
        if(whiteDuration > 0) sprite.GetComponent<SpriteRenderer>().color = Color.white;
        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        burst.count = damage * damageParticleBurst;
        particle.emission.SetBurst(0, burst);
        particle.Play();
        timer = 0;
        if (health <= 0)
        {
            parent.ComponentDestroyed(GetComponent<EnemyComponent>(), isCore);
            GetComponent<EnemyComponent>().StopAllCoroutines();
            Destroy(gameObject);
        }
    }
    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        sprite.transform.localScale = new Vector3(health / maxHealth, health / maxHealth, 1);
    }
}
