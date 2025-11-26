using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffScript : MonoBehaviour
{
    private BuffData data;
    private int cost;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void NewBuff(BuffData data)
    {
        this.data = data;
        cost = data.cost;
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = data.colorSet.normalColor;
        colorBlock.highlightedColor = data.colorSet.highlightedColor;
        colorBlock.pressedColor = data.colorSet.pressedColor;
        colorBlock.selectedColor = data.colorSet.normalColor;
        description.color = data.colorSet.textColor;
        costText.color = data.colorSet.textColor;
        description.fontSize = data.fontSize;
        button.colors = colorBlock;
        description.text = data.description;
        costText.text = data.cost.ToString();
    }
    public void BuyBuff()
    {
        if (cost <= BuffManager.Instance.buffPoints)
        {
            BuffManager.Instance.buffPoints -= cost;
            PlayerHealth.Instance.healthBuff += data.healthBuff;
            PassiveDamage.Instance.Upgrade(data.passiveDamageBuff);
            Shooting.Instance.primaryDamageBuff += data.primaryDamageBuff;
            Shooting.Instance.primaryFireRateBuff += data.primaryFireRateBuff;
            Shooting.Instance.primaryAmmoBuff += data.primaryAmmoBuff;
            Shooting.Instance.primaryExplosionRadiusBuff += data.primaryExplosionRadiusBuff;
            Shooting.Instance.primaryLifesteal += data.primaryLifestealBuff;
            Shooting.Instance.secondaryDamageBuff += data.secondaryDamageBuff;
            Shooting.Instance.secondaryAmmoBuff += data.secondaryAmmoBuff;
            Shooting.Instance.secondaryExplosionRadiusBuff += data.secondaryExplosionRadiusBuff;
            Shooting.Instance.secondaryReloadTimeReductionBuff += data.secondaryReloadTimeReductionBuff;
            Shooting.Instance.Reload();
            button.interactable = true;
            BuffManager.Instance.UpdatePoints();
            BuffManager.Instance.Reroll();
        }
    }
}
