using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Vector3 target;
    private float timer;
    private float speed;
    private float duration;
    private float damage;
    private Vector2 size;
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
        target = Shooting.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
