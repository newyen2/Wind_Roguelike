using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ArchiveWindPrefab: MonoBehaviour
{
    public GameObject upIcon;
    public GameObject downIcon;
    public GameObject leftIcon;
    public GameObject rightIcon;
    public Text nameText;
    public Text description;
    public Text cost;

    public CardData wind;

    public void changeData(CardData windData)
    {
        wind = windData;
        upIcon.SetActive(windData.direction.up);
        downIcon.SetActive(windData.direction.down);
        leftIcon.SetActive(windData.direction.left);
        rightIcon.SetActive(windData.direction.right);
        nameText.text = windData.displayName;
        description.text = windData.description;
        cost.text = windData.baseCost.ToString();
        GetComponent<Image>().sprite = windData.Image;
    }
}