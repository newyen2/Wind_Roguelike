using TMPro;
using UnityEngine;
public class DeckPileCounterUI : MonoBehaviour
{

    [SerializeField] private TMP_Text drawCountText;
    [SerializeField] private TMP_Text discardCountText;

    void Update()
    {
        // DeckManager 還沒生成、不在戰鬥場景 → 直接跳過（不報錯）
        if (DeckManager.Instance == null)
            return;

        // 避免 inspector 尚未設 reference 造成 NullReference
        if (drawCountText != null)
            drawCountText.text = DeckManager.Instance.DrawPile.Count.ToString();

        if (discardCountText != null)
            discardCountText.text = DeckManager.Instance.DiscardPile.Count.ToString();
    }
}