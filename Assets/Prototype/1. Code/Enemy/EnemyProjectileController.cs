using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Vector3 target;
    private float timer;
    private float speed;
    private float duration;
    private float damage;
    private Vector2 size;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void Initialize(float speed, float duration, float damage, Vector2 size)
    {
        this.speed = speed;
        this.duration = duration;
        this.damage = damage;
        transform.localScale = size;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = Shooting.Instance.transform.position;
        float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector3 direction = transform.rotation * Vector3.right;
        transform.position += direction * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= duration) Destroy(gameObject);
        if (transform.position == target) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall")) Destroy(gameObject);
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
