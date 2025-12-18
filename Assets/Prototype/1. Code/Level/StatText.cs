using TMPro;
using UnityEngine;

public class StatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] stats stat;
    public void UpdateText()
    {
        if (stat == stats.Primary)
        text.text =
            $"{Shooting.Instance.primaryDamageBuff} Primary Damage\n" +
            $"{Shooting.Instance.primaryFireRateBuff} Primary Fire Rate\n" +
            $"{Shooting.Instance.primaryAmmoBuff} Primary Ammo\n" +
            $"{Shooting.Instance.primaryLifesteal} Primary Lifesteal\n" +
            $"{Shooting.Instance.primaryCoreDamageBuff} Primary Damage vs Cores\n";
        else if (stat == stats.Secondary)
            text.text =
                ($"{Shooting.Instance.secondaryDamageBuff} Secondary Damage\n" +
                $"{Shooting.Instance.secondaryExplosionRadiusBuff} Secondary Explosion Radius\n" +
                $"{Shooting.Instance.secondaryAmmoBuff} Secondary Ammo\n" +
                $"{Shooting.Instance.secondaryReloadTimeReductionBuff} Secondary Reload Time Reduction\n" +
                $"{Shooting.Instance.secondaryDamageOverTime} Secondary Damage Over Time/s\n");
        else if (stat == stats.Other)
            text.text =
                $"{PlayerHealth.Instance.healthBuff} Health\n" +
                $"{PassiveDamage.Instance.damage} Passive Damage/s\n";
    }
    private enum stats
    { 
        Primary,
        Secondary,
        Other
    }
}