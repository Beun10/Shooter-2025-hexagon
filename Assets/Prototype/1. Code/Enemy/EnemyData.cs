using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public GameObject prefab {  get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public Color color { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public int spawnChance { get; private set; }
    [field: SerializeField] public int spawnWeight { get; private set; }
    [field: SerializeField] public Vector2 hitbox {  get; private set; }
    [field: SerializeField] public Vector2 size { get; private set; }
    [field: SerializeField] public bool component {  get; private set; }
    [field: SerializeField] public Material particleMaterial { get; private set; }
}
