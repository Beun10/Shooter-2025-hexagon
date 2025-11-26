using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting Instance;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private ParticleSystem particle;
    //Primary
    [SerializeField] private GameObject primaryBullet;
    [SerializeField] private GameObject primaryAmmoSprite;
    [SerializeField] private Color primaryAmmoColor;
    [SerializeField] private float primaryWhiteDuration;
    [SerializeField] private float primaryFireRate;
    [SerializeField] private float primaryMaxAmmo;
    [SerializeField] private float primaryReloadTime;
    [SerializeField] private float primaryDamage;
    [SerializeField] private float primaryExplosionRadius;
    [SerializeField] private float primaryBulletDuration;
    [SerializeField] private float primaryBulletSpeed;
    public float primaryLifesteal;
    public float primaryDamageBuff = 1;
    public float primaryFireRateBuff = 1;
    public float primaryAmmoBuff = 1;
    public float primaryExplosionRadiusBuff = 1;
    private float primaryReloadTimer;
    private float timerPrimary;
    private float whiteTimerPrimary;
    private bool canShootPrimary = true;
    private float primaryCurrentAmmo;

    //Secondary
    [SerializeField] private GameObject secondaryBullet;
    [SerializeField] private GameObject secondaryAmmoSprite;
    [SerializeField] private Color secondaryAmmoColor;
    [SerializeField] private float secondaryWhiteDuration;
    [SerializeField] private float secondaryFireRate;
    [SerializeField] private float secondaryMaxAmmo;
    [SerializeField] private float secondaryReloadTime;
    [SerializeField] private float secondaryDamage;
    [SerializeField] private float secondaryExplosionRadius;
    [SerializeField] private float secondaryBulletDuration;
    [SerializeField] private float secondaryBulletSpeed;
    public float secondaryLifesteal;
    public float secondaryDamageBuff = 1;
    public float secondaryAmmoBuff = 1;
    public float secondaryExplosionRadiusBuff = 1;
    public float secondaryReloadTimeReductionBuff = 1;
    private float secondaryReloadTimer;
    private float timerSecondary;
    private float whiteTimerSecondary;
    private bool canShootSecondary = true;
    private float secondaryCurrentAmmo;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        primaryCurrentAmmo = primaryMaxAmmo;
        secondaryCurrentAmmo = secondaryMaxAmmo;
        primaryAmmoSprite.GetComponent<SpriteRenderer>().color = primaryAmmoColor;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && canShootPrimary)
        {
            GameObject bullet = Instantiate(primaryBullet, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            bullet.GetComponent<BulletController>().Initialize(primaryDamage * primaryDamageBuff, primaryExplosionRadius * primaryExplosionRadiusBuff, primaryBulletSpeed, primaryBulletDuration, primaryLifesteal);
            canShootPrimary = false;
            timerPrimary = 0;
            primaryCurrentAmmo -= 1;
            primaryAmmoSprite.GetComponent<SpriteRenderer>().color = Color.white;
            Weapon.GetComponent<Animation>().Play();
            whiteTimerPrimary = 0;
            primaryAmmoSprite.transform.localScale = new Vector3(primaryCurrentAmmo / (primaryMaxAmmo * primaryAmmoBuff), primaryCurrentAmmo / (primaryMaxAmmo * primaryAmmoBuff), 1);
            particle.Play();
            if (primaryCurrentAmmo <= 0) primaryReloadTimer = 0;
        }
        if (Input.GetButton("Fire3") && canShootSecondary)
        {
            GameObject bullet = Instantiate(secondaryBullet, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            bullet.GetComponent<BulletController>().Initialize(secondaryDamage * secondaryDamageBuff, secondaryExplosionRadius * secondaryExplosionRadiusBuff, secondaryBulletSpeed, secondaryBulletDuration, secondaryLifesteal);
            canShootSecondary = false;
            timerSecondary = 0;
            secondaryCurrentAmmo -= 1;
            secondaryAmmoSprite.GetComponent<SpriteRenderer>().color = Color.white;
            Weapon.GetComponent<Animation>().Play();
            whiteTimerSecondary = 0;
            secondaryAmmoSprite.transform.localScale = new Vector3(secondaryCurrentAmmo / (secondaryMaxAmmo * secondaryAmmoBuff), secondaryCurrentAmmo / (secondaryMaxAmmo * secondaryAmmoBuff), 1);
            if (secondaryCurrentAmmo <= 0) secondaryReloadTimer = 0;
        }
        TimersTick();
    }
    private void TimersTick()
    {
        timerPrimary += Time.deltaTime;
        timerSecondary += Time.deltaTime;
        if (whiteTimerPrimary < primaryWhiteDuration) whiteTimerPrimary += Time.deltaTime;
        if (whiteTimerSecondary < secondaryWhiteDuration) whiteTimerSecondary += Time.deltaTime;
        if (primaryCurrentAmmo <= 0) primaryReloadTimer += Time.deltaTime;
        if (secondaryCurrentAmmo <= 0) secondaryReloadTimer += Time.deltaTime;
        if (timerPrimary > 1 / primaryFireRate / primaryFireRateBuff && primaryCurrentAmmo > 0) canShootPrimary = true;
        if (timerSecondary > 1 / secondaryFireRate && secondaryCurrentAmmo > 0) canShootSecondary = true;
        if (primaryReloadTimer >= primaryReloadTime)
        {
            primaryReloadTimer = 0;
            primaryCurrentAmmo = primaryMaxAmmo * primaryAmmoBuff;
            primaryAmmoSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        if (secondaryReloadTimer >= secondaryReloadTime / secondaryReloadTimeReductionBuff)
        {
            secondaryReloadTimer = 0;
            secondaryCurrentAmmo = secondaryMaxAmmo * secondaryAmmoBuff;
            secondaryAmmoSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        if (whiteTimerPrimary >= primaryWhiteDuration) primaryAmmoSprite.GetComponent<SpriteRenderer>().color = primaryAmmoColor;
        if (whiteTimerSecondary >= secondaryWhiteDuration) secondaryAmmoSprite.GetComponent<SpriteRenderer>().color = secondaryAmmoColor;
    }

    public void Reload()
    {
        primaryReloadTimer = 100;
        secondaryReloadTimer = 100;
        health.TakeDamage(-9999);
    }
}
