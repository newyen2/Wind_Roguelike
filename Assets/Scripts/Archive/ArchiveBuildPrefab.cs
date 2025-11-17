using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core;
using TMPro;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class ArchiveBuildPrefab : MonoBehaviour
{
    public GameObject reward = null;

    public void changeData(GameObject gameObject)
    {
        reward = gameObject;
        if(this.name == "IgnoreButton") return;
        reward = gameObject;
        transform.GetChild(0).GetComponent<Image>().sprite = reward.GetComponent<Image>().sprite;
        transform.GetChild(1).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().displayName;
        transform.GetChild(2).GetComponent<TMP_Text>().text = reward.GetComponent<BuildingBase>().description;
    }
}