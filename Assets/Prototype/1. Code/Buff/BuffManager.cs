using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance;
    public float buffPoints;
    [SerializeField] private int baseBuffPoints;
    [SerializeField] private GameObject buff;
    public float levelBuffPoints;
    [SerializeField] private TextMeshPro pointsText;
    private List<GameObject> buffs = new List<GameObject>();
    [SerializeField] private List<BuffTierData> buffTiers = new List<BuffTierData>();
    [SerializeField] private List<int> buffTierProbability;
    [SerializeField] private Vector2 buffPosition;
    [SerializeField] private Vector2 buffOffset;
    [SerializeField] private int buffCount;
    [SerializeField] private List<GameObject> statText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buffPoints = baseBuffPoints;
        pointsText.text = buffPoints.ToString() + " Points";
        Vector2 currentBuffPosition = buffPosition;
        for (int i = 0; i < buffCount; i++)
        {
            buffs.Add(Instantiate(buff, currentBuffPosition, Quaternion.identity));
            currentBuffPosition += buffOffset;
        }
        Reroll();
    }
    public void Reroll()
    {
        foreach (GameObject buffObject in buffs)
        {
            List<BuffData> selectedTier = SelectTier();
            BuffData NewBuff = selectedTier[UnityEngine.Random.Range(0, selectedTier.Count)];
            buffObject.GetComponent<BuffScript>().NewBuff(NewBuff);
        }
    }

    public void RerollOne(GameObject buff)
    {
        List<BuffData> selectedTier = SelectTier();
        BuffData NewBuff = selectedTier[UnityEngine.Random.Range(0, selectedTier.Count)];
        buff.GetComponent<BuffScript>().NewBuff(NewBuff);
    }

    public void UpdatePoints()
    {
        pointsText.text = buffPoints.ToString() + " Points";
        foreach(GameObject text in statText) text.GetComponent<StatText>().UpdateText();
    }
    public void ToggleButtons(bool active)
    {
        foreach (GameObject buffObject in buffs)
        {
            buffObject.SetActive(active);
        }
    }

    private List<BuffData> SelectTier()
    {
        int i = 0;
        int randomNumber = Random.Range(1, 101);
        while (true)
        {
            if (randomNumber <= buffTierProbability[i]) break;
            else i++;
            if (i > buffTierProbability.Count) break;
        }
        List<BuffData> selectedTier = buffTiers[i].buffs;
        return selectedTier;
    }
}
