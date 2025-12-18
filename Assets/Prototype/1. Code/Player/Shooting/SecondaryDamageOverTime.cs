using UnityEngine;

public class SecondaryDamageOverTime : MonoBehaviour
{
    private float damage;
    private float duration = 1;
    private float timer;
    [SerializeField] private float areaOfEffectMutliplier;
    public void Initialize(float damage, float duration, float size, Vector3 position)
    {
        this.damage = damage;
        this.duration = duration;
        transform.localScale = new Vector2(size * areaOfEffectMutliplier, size * areaOfEffectMutliplier);
        transform.position = position;
        if (damage == 0) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration) Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage * Time.deltaTime, Shooting.Instance.secondaryLifesteal, GameManager.DamageType.Secondary);
        }
    }
}
