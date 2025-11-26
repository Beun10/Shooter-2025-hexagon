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
    private float lifesteal;
    [SerializeField] private bool explosive;
    private float explosionRadius;
    [SerializeField] private CircleCollider2D hitbox;
    [SerializeField] private float explosionDuration;
    private Rigidbody2D rb;
    private GameObject pointer;
    private Vector2 target;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(float damage, float explosionRadius, float speed, float duration, float lifesteal)
    {
        this.damage = damage;
        this.explosionRadius = explosionRadius;
        this.speed = speed; 
        this.duration = duration;
        this.lifesteal = lifesteal;
    }
    void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        pointer = GameObject.Find("Pointer");
        target = pointer.transform.position;
        timer = 0;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= HitboxActivationDelay) GetComponent<CircleCollider2D>().enabled = true;
        if (timer >= duration) Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall")) Explosion();
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, lifesteal);
            Explosion();
        }
    }

    private void Explosion()
    {
        if (explosive)
        {
            hitbox.isTrigger = true;
            transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
            speed = 0;
            timer = 0;
            duration = explosionDuration;
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, lifesteal);
    }
}