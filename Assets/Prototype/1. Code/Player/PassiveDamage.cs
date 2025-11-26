using UnityEngine;

public class PassiveDamage : MonoBehaviour
{
    public static PassiveDamage Instance;
    [SerializeField] private float damageInterval;
    [SerializeField] private float alpha;
    [SerializeField] private float alphaDamageMultiplier;
    [SerializeField] private float maxAlpha;
    [SerializeField] private GameObject areaOfEffect;
    [SerializeField] private Color AoeColor;
    [SerializeField] private float damageRange;
    [SerializeField] private GameObject player;
    public float damage;
    public float lifesteal;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        areaOfEffect.transform.localScale = new Vector3(damageRange, damageRange, 1);
    }

    // Update is called once per frame
    void Update()
    {
        areaOfEffect.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
    }
    public void Upgrade(float damage)
    {
        this.damage += damage;
        alpha = this.damage * alphaDamageMultiplier;
        if (alpha >  maxAlpha) alpha = maxAlpha;
        AoeColor.a = alpha / 255;
        areaOfEffect.GetComponent<SpriteRenderer>().color = AoeColor;
    }
}
