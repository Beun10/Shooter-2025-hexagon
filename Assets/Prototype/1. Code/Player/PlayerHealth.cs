using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] private Transform healthSprite;
    [SerializeField] private float maxHealth;
    public float healthBuff = 1f;
    private float health;
    [SerializeField] private float whiteDuration;
    [SerializeField] private Color baseColor;
    [SerializeField] private ParticleSystem particle;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        health = maxHealth;
        particle.startColor = baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < whiteDuration)
        {
            timer += Time.deltaTime;
            if (timer >= whiteDuration) healthSprite.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > maxHealth * healthBuff) health = maxHealth * healthBuff;
        healthSprite.localScale = new Vector3 (health / (maxHealth * healthBuff), health / (maxHealth * healthBuff), 1);
        if (health <= 0) SceneManager.LoadScene("SampleScene");
        if (damage > 0)
        {
            healthSprite.GetComponent<SpriteRenderer>().color = Color.white;
            particle.Play();
        }
        timer = 0;
    }
}
