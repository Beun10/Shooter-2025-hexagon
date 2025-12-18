using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "ScriptableObject/Buff/Buff Data")]
public class BuffData : ScriptableObject
{
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public BuffColorSet colorSet { get; private set; }
    [field: SerializeField] public string description { get; private set; }
    [field: SerializeField] public float fontSize { get; private set; }
    //General
    [field: SerializeField] public float healthBuff { get; private set; }
    [field: SerializeField] public float passiveDamageBuff { get; private set; }
    //Primary
    [field: SerializeField] public float primaryDamageBuff { get; private set; }
    [field: SerializeField] public float primaryFireRateBuff { get; private set; }
    [field: SerializeField] public float primaryAmmoBuff { get; private set; }
    [field: SerializeField] public float primaryExplosionRadiusBuff { get; private set; }
    [field: SerializeField] public float primaryLifestealBuff {  get; private set; }
    [field:SerializeField] public float primaryCoreDamageBuff {  get; private set; }
    //Secondary
    [field :SerializeField] public float secondaryDamageBuff { get; private set; }
    [field: SerializeField] public float secondaryAmmoBuff { get; private set; }
    [field: SerializeField] public float secondaryExplosionRadiusBuff { get; private set; }
    [field: SerializeField] public float secondaryReloadTimeReductionBuff { get; private set; }
    [field: SerializeField] public float secondaryDamageOverTimeBuff { get; private set; }
}
