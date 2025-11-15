using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;

public class RewardCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject reward;

    void Start()
    {
        GlobalManager.Instance.rewardBuild = new();
        RenewReward(reward);
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