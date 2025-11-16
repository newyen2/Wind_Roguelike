using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;

public class RewardCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject reward = null;
    public RewardCard[] otherRewardCard;
    public bool is_random = true;

    void Start()
    {
        GlobalManager.Instance.rewardBuild = new();
        RenewReward(Random());
    }
    GameObject Random()
    {
        if(!is_random) return reward;

        GameObject[] gameObjects = GlobalManager.Instance.buildingPrefabs;
        // collect only prefabs that are allowed by recordsCount
        List<GameObject> available = new List<GameObject>();
        foreach(GameObject gb in gameObjects)
        {
            if (gb.GetComponent<BuildingBase>().max_round >= GlobalManager.Instance.recordsCount)
            if (gb.GetComponent<BuildingBase>().min_round <= GlobalManager.Instance.recordsCount)
            if (gb != otherRewardCard[0])
            if (gb != otherRewardCard[1])
            {
                available.Add(gb);
            }
        }

        if (available.Count == 0) return null;

        // pick a weighted random available prefab and set it as the reward
        float totalWeight = 0f;
        foreach(GameObject gb in available)
        {
            totalWeight += gb.GetComponent<BuildingBase>().weight;
        }
        
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float currentWeight = 0f;
        foreach(GameObject gb in available)
        {
            currentWeight += gb.GetComponent<BuildingBase>().weight;
            if(randomValue <= currentWeight)
            {
                RenewReward(gb);
                return gb;
            }
        }
        
        return available[available.Count - 1];
    }

    public void RenewReward(GameObject gameObject)
    {
        if(this.name == "IgnoreButton") return;
        reward = gameObject;
        transform.GetChild(0).GetComponent<Image>().sprite = reward.GetComponent<Image>().sprite;
        transform.GetChild(1).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().displayName;
        transform.GetChild(2).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(reward != null) GlobalManager.Instance.rewardBuild.Add(reward);
        GameManager.Instance.SwitchScene("Building");
    }
}