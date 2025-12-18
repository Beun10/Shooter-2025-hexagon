using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameManager.DamageType GetDamageType(string damageType)
    {
        if (damageType == "Primary") return DamageType.Primary;
        if (damageType == "Secondary") return DamageType.Secondary;
        else return DamageType.Other;
    }
    public enum DamageType
    {
        Primary,
        Secondary,
        Other
    }

    
}