using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Buff Tier", menuName = "ScriptableObject/Buff/Buff Tier")]
public class BuffTierData : ScriptableObject
{
    [field: SerializeField] public List<BuffData> buffs {  get; private set; }
}
