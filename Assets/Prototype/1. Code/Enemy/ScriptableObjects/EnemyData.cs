using NUnit.Framework;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public GameObject prefab {  get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public int spawnChance { get; private set; }
    [field: SerializeField] public int spawnWeight { get; private set; }
    [field: SerializeField] public Vector2 size { get; private set; }
    [field: SerializeField] public bool component {  get; private set; }

    [field: SerializeField] public float rotatingSpeed { get; private set; }
    [field: SerializeField] public EnemyComponentData armor { get; private set; }
    [field: SerializeField] public EnemyComponentData core { get; private set; }
    [field: SerializeField] public float minimalDistance { get; private set; }
    [field: SerializeField] public EnemyPattern[] patterns { get; private set; }
    [field: SerializeField] public float firstPatternDelay { get; private set; }
    [field: SerializeField] public float patternCooldown { get; private set; }
    [field: SerializeField] public Vector2 patternCooldownVariation { get; private set; }
}
