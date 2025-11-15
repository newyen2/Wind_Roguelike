using UnityEngine;
using UnityEngine.EventSystems;
using Core;

public class RewardCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject reward;

    void Start()
    {
        GlobalManager.Instance.rewardBuild = new();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalManager.Instance.rewardBuild.Add(reward);
        GameManager.Instance.SwitchScene("Building");
    }
}