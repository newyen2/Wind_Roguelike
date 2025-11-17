using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class RewardCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject reward = null;
    public RewardCard[] otherRewardCard;
    public bool is_random = true;
    public draw drawer;
    public int id;

    void Start()
    {
        if (is_random)
        {
            StartCoroutine(show());
        }
        else
        {
            GlobalManager.Instance.rewardBuild = new();
        RenewReward(reward);

        }
    }
    IEnumerator show()
    {
        yield return new WaitUntil(()=>draw.Instance.finish);

            reward = drawer.rewards[id];
        GlobalManager.Instance.rewardBuild = new();
            RenewReward(reward);
        

    }

    public void RenewReward(GameObject gameObject)
    {
        reward = gameObject;
        if(this.name == "IgnoreButton") return;
        reward = gameObject;
        transform.GetChild(0).GetComponent<Image>().sprite = reward.GetComponent<Image>().sprite;
        transform.GetChild(1).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().displayName;
        transform.GetChild(2).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(reward != null) GlobalManager.Instance.rewardBuild.Add(reward);
        if(this.name == "Init") GameManager.Instance.SwitchScene("Building");
        else GameManager.Instance.SwitchScene("WindResult");
    }
}