using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
[System.Serializable]
public struct DirectionSpritePair
{
    public CardDirection direction;
    public Sprite sprite;
}
public class CardView : MonoBehaviour
{
    [Header("UI 元件")]
    public Image directionImage;
    public Text nameText;
    public Text descriptionText;
    public Text costText;
    public List<DirectionSpritePair> directionSprites;
    private Dictionary<CardDirection, Sprite> directionMap;
    
    //public Text windPowerText;

    public CardInstance instance;
    private void Awake()
    {
        // List → Dictionary
        directionMap = new Dictionary<CardDirection, Sprite>();
        foreach (var pair in directionSprites)
        {
            directionMap[pair.direction] = pair.sprite;
        }
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
    private void UpdateDirectionIcon(CardDirection dir)
    {
        if (directionImage == null) return;

        if (directionMap.TryGetValue(dir, out Sprite sprite))
        {
            directionImage.sprite = sprite;
            directionImage.enabled = true;
        }
        else
        {
            directionImage.enabled = false; // 無方向就關閉
        }
    }
}