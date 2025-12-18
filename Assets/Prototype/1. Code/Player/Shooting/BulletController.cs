using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class BulletController : MonoBehaviour
{
    private float speed;
    private float duration;
    [SerializeField] private float HitboxActivationDelay;
    private float damage;
    private float damageOverTime;
    private float damageOverTimeDuration;
    private float lifesteal;
    [SerializeField] private bool explosive;
    private float explosionRadius;
    [SerializeField] private CircleCollider2D hitbox;
    [SerializeField] private float explosionDuration;
    [SerializeField] private float particleDuration;
    [SerializeField] private DamageType damageType;
    [SerializeField] private GameObject prefab;
    private Rigidbody2D rb;
    private GameObject pointer;
    private Vector2 target;
    private float timer;
    private bool exploded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(float damage, float explosionRadius, float speed, float duration, float lifesteal)
    {
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.speed = speed; 
        this.duration = duration;
        this.lifesteal = lifesteal;
        damageOverTime = Shooting.Instance.secondaryDamageOverTime;
        damageOverTimeDuration = Shooting.Instance.secondaryDamageOverTimeDuration;
    }
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        pointer = GameObject.Find("Pointer");
        target = pointer.transform.position;
        timer = 0;
        exploded = false;
        float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= HitboxActivationDelay) GetComponent<CircleCollider2D>().enabled = true;
        if (timer >= duration)
        {
            if (duration != particleDuration)
            {
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                damage = 0;
                timer = 0;
                duration = particleDuration;
            }
            else Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!exploded && collision.gameObject.tag.Equals("Wall")) Explosion();
    }

    private void Explosion()
    {
        if (explosive)
        {
            exploded = true;
            transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
            speed = 0;
            timer = 0;
            duration = explosionDuration;
            GetComponent<ParticleSystem>().Play();
            if (prefab != null)
            {
                GameObject explosion = Instantiate(prefab);
                explosion.GetComponent<SecondaryDamageOverTime>().Initialize(damageOverTime, damageOverTimeDuration, explosionRadius, transform.position);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!exploded && collision.gameObject.tag.Equals("Wall")) Explosion();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, lifesteal, GameManager.Instance.GetDamageType(damageType.ToString()));
            if(!exploded) Explosion();
        }
    }

    private enum DamageType 
    { 
        Primary,
        Secondary,
        Other
    }
}