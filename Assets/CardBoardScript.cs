using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBoardScript : MonoBehaviour
{
    public static CardBoardScript Instance { get; private set; }
    public GameObject fakeWindObject;
    public FakeWind fakeWind;
    public Image cardImage;
    public Image boardImage;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        boardImage.enabled = false;
        fakeWindObject.SetActive(false);
    }

    public void ShowCard(CardInstance card, Image artImage)
    {
        boardImage.enabled = true;
        fakeWindObject.SetActive(true);

        fakeWind.nameText.text = card.data.displayName;
        fakeWind.description.text = card.data.description;
        fakeWind.cost.text = card.currentCost.ToString();
        cardImage.sprite = artImage.sprite;

        fakeWind.upIcon.SetActive(card.data.direction.down == true);
        fakeWind.downIcon.SetActive(card.data.direction.up == true);
        fakeWind.leftIcon.SetActive(card.data.direction.right == true);
        fakeWind.rightIcon.SetActive(card.data.direction.left == true);
    }

    public void HideCard()
    {
        boardImage.enabled = false;
        fakeWindObject.SetActive(false);
    }
}
