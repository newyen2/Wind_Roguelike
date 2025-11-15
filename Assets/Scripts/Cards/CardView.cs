using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [Header("UI 元件")]
    public Image artworkImage;
    public Text nameText;
    public Text descriptionText;
    public Text costText;
    //public Text windPowerText;

    [HideInInspector] public CardInstance instance;

    public void Setup(CardInstance instance)
    {
        if (instance == null)
        {
            instance = CreateDummyInstance();
        }
        Apply(instance);
    }
    private CardInstance CreateDummyInstance()
    {
        CardData fake = ScriptableObject.CreateInstance<CardData>();
        fake.displayName = "Test Card";
        fake.description = "Test Description";
        fake.baseCost = 3;

        return new CardInstance(fake);
    }

    private void Apply(CardInstance instance)
    {
        this.instance = instance;
        var data = instance.data;

        artworkImage.sprite = data.Image;
        nameText.text = data.displayName;
        descriptionText.text = data.description;
        costText.text = instance.currentCost.ToString();
    }

    // 例：點擊時打出 / 丟棄
    public void OnClick_PlayCard()
    {
        // 這裡先示範「打出 → 丟到棄牌堆」
        DeckManager.Instance.DiscardFromHand(instance);

        // 之後可改成：
        // GameManager.Instance.TryPlayCard(instance);
    }
    void Start()
    {
    #if UNITY_EDITOR
        if (instance == null)
            Setup(null);    // 自動建假卡
    #endif
    }
}