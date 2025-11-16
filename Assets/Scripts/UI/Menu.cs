using System.Collections;
using Core;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject contributor;

    void Start()
    {
        contributor.SetActive(false);
    }

    public void ContributorButton()
    {
        if(!contributor.activeSelf) contributor.SetActive(true);
        else contributor.SetActive(false);
    }
}