using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WindRewardCard : MonoBehaviour, IPointerClickHandler
{
    public CardData reward = null;
    public WindRewardCard[] otherRewardCard;
    public Image upIcon;
    public Image downIcon;
    public Image leftIcon;
    public Image rightIcon;

    public bool is_random = true;

    void Start()
    {
        RenewReward(Random());
    }
    CardData Random()
    {
        CardData[] gameObjects = GlobalManager.Instance.windPrefabs;
        // collect only prefabs that are allowed by recordsCount
        List<CardData> available = new List<CardData>();
        foreach(CardData gb in gameObjects)
        {
            if(!is_random) return reward;

            if (gb.max_round >= GlobalManager.Instance.recordsCount)
            if (gb.min_round <= GlobalManager.Instance.recordsCount)
            if (gb != otherRewardCard[0])
            if (gb != otherRewardCard[1])
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
        print( transform.GetComponent<Image>());
        transform.GetComponent<Image>().sprite = reward.Image;
        transform.GetChild(1).GetComponent<Text>().text = reward.displayName;
        transform.GetChild(2).GetComponent<Text>().text = reward.description;
        transform.GetChild(3).GetComponent<Text>().text = reward.baseCost.ToString();
        UpdateDirectionIcons(gameObject.direction);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(reward != null) DeckManager.Instance.startingDeck.Add(reward);
        GameManager.Instance.SwitchScene("Building");
    }

    private void UpdateDirectionIcons(CardDirection direction) //往下吹的風，應該是上面的方向量，風向定義是這樣的w
    {
        if (upIcon    != null) upIcon.enabled    = direction.down;
        if (downIcon  != null) downIcon.enabled  = direction.up;
        if (leftIcon  != null) leftIcon.enabled  = direction.right;
        if (rightIcon != null) rightIcon.enabled = direction.left;
    }
}