using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[System.Serializable]
public class CardView : MonoBehaviour
{
    [Header("UI 元件")]
    public Image upIcon;
    public Image downIcon;
    public Image leftIcon;
    public Image rightIcon;
    
    public Image ArtImage;
    public Text nameText;
    public Text descriptionText;
    public Text costText;

    public CardInstance instance;
    private void Awake()
    {
        if (ArtImage == null)
            ArtImage = GetComponent<Image>();
    }
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

        nameText.text = data.displayName;
        descriptionText.text = data.description;
        costText.text = instance.currentCost.ToString();
        if (ArtImage != null && data.Image != null)
        {
            ArtImage.sprite = data.Image;
        }


        UpdateDirectionIcons(data.direction);

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
    private void UpdateDirectionIcons(CardDirection direction) //往下吹的風，應該是上面的方向量，風向定義是這樣的w
    {
        if (upIcon    != null) upIcon.enabled    = direction.down;
        if (downIcon  != null) downIcon.enabled  = direction.up;
        if (leftIcon  != null) leftIcon.enabled  = direction.right;
        if (rightIcon != null) rightIcon.enabled = direction.left;
    }
}