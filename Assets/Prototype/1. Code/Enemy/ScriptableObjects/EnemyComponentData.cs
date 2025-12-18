using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy/Enemy Component Data")]
public class EnemyComponentData : ScriptableObject
{
    [field: SerializeField] public int health { get; private set; }
    [field: SerializeField] public int damage { get; private set; }
    [field: SerializeField] public float attackCooldown { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Color color { get; private set; }
    [field: SerializeField] public Vector2 hitbox { get; private set; }
    [field: SerializeField] public Vector2 size { get; private set; }
    [field: SerializeField] public GameObject projectile { get; private set; }
    [field: SerializeField] public float projectileSpeed { get; private set; }
    [field: SerializeField] public Vector2 projectileSize { get; private set; }
    [field: SerializeField] public float projectileDuration { get; private set; }
    [field: SerializeField] public Material particleMaterial { get; private set; }
    [field: SerializeField] public bool isCore { get; private set; }
    [field: SerializeField] public float primaryDamageMultiplier { get; private set; }
    [field: SerializeField] public float secondaryDamageMultiplier { get; private set; }
    [field: SerializeField] public float healingSpeed {  get; private set; }
    [field: SerializeField] public float decayRate { get; private set; }
}
