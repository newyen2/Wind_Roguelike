using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;
using System.Collections.Generic;

public class WindRewardCard : MonoBehaviour, IPointerClickHandler
{
    public CardData reward = null;
    public WindRewardCard[] otherRewardCard;
    public List<DirectionSpritePair> directionSprites;
    private Dictionary<CardDirection, Sprite> directionMap;
    public bool is_random = true;

    void Start()
    {
        directionMap = new Dictionary<CardDirection, Sprite>();
        foreach (var pair in directionSprites)
        {
            directionMap[pair.direction] = pair.sprite;
        }
        
        RenewReward(Random());
    }
    CardData Random()
    {
        CardData[] gameObjects = GlobalManager.Instance.windPrefab;
        // collect only prefabs that are allowed by recordsCount
        List<CardData> available = new List<CardData>();
        foreach(CardData gb in gameObjects)
        {
            if(!is_random) return reward;

            if (gb.max_round >= GlobalManager.Instance.recordsCount)
            if (gb.min_round <= GlobalManager.Instance.recordsCount)
            if (gb != otherRewardCard[0].reward)
            if (gb != otherRewardCard[1].reward)
            {
                available.Add(gb);
            }
        }

        if (available.Count == 0) return null;

        // pick a weighted random available prefab and set it as the reward
        float totalWeight = 0f;
        foreach(CardData gb in available)
        {
            totalWeight += gb.weight;
        }
        
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float currentWeight = 0f;
        foreach(CardData gb in available)
        {
            currentWeight += gb.weight;
            if(randomValue <= currentWeight)
            {
                RenewReward(gb);
                return gb;
            }
        }
        
        return available[available.Count - 1];
    }

    public void RenewReward(CardData gameObject)
    {
        reward = gameObject;
        if(this.name == "IgnoreButton") return;
        transform.GetComponent<Image>().sprite = reward.Image;
        transform.GetChild(1).GetComponent<Text>().text = reward.displayName;
        transform.GetChild(2).GetComponent<Text>().text = reward.description;
        transform.GetChild(3).GetComponent<Text>().text = reward.baseCost.ToString();
        if (directionMap.TryGetValue(reward.direction, out Sprite sprite))
        {
            transform.GetChild(4).GetComponent<Image>().sprite = sprite;
            transform.GetChild(4).GetComponent<Image>().enabled = true;
        }
        else
        {
            transform.GetChild(4).GetComponent<Image>().enabled = false; // 無方向就關閉
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(reward != null) DeckManager.Instance.startingDeck.Add(reward);
        GameManager.Instance.SwitchScene("Building");
    }
}