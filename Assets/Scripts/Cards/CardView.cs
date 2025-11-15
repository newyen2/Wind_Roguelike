using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [Header("UI 元件")]
    public Image artworkImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text windPowerText;

    [HideInInspector] public CardInstance instance;

    public void Setup(CardInstance instance)
    {
        this.instance = instance;

        var data = instance.data;
        if (artworkImage != null) artworkImage.sprite = data.Image;
        if (nameText != null)     nameText.text = data.displayName;
        if (descriptionText != null) descriptionText.text = data.description;
        if (costText != null)     costText.text = instance.currentCost.ToString();
        if (windPowerText != null) windPowerText.text = instance.currentWindPower.ToString();
    }

    // 例：點擊時打出 / 丟棄
    public void OnClick_PlayCard()
    {
        // 這裡先示範「打出 → 丟到棄牌堆」
        DeckManager.Instance.DiscardFromHand(instance);

        // 之後可改成：
        // GameManager.Instance.TryPlayCard(instance);
    }
}