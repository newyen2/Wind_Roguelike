using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//手牌列表＋UI 生成
public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    [Header("UI 相關")]
    public Transform handRoot;   // 手牌的父物件（通常是某個 Horizontal Layout）
    public CardView cardViewPrefab; // 代表一張牌的 UI 預製物

    private readonly List<CardInstance> hand = new List<CardInstance>();
    private readonly List<CardView> cardViews = new List<CardView>();

    public IReadOnlyList<CardInstance> CardsInHand => hand;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// 新增一張牌到手牌（邏輯 + UI）
    /// </summary>
    public void AddCardToHand(CardInstance instance)
    {
        hand.Add(instance);

        var view = Instantiate(cardViewPrefab, handRoot);
        view.Setup(instance);
        cardViews.Add(view);

        // 之後可以在這裡呼叫 ReorderHand() 排列位置
    }

    /// <summary>
    /// 從手牌中移除一張牌（邏輯 + UI）並回傳是否成功移除
    /// </summary>
    public bool RemoveCardFromHand(CardInstance instance)
    {
        int index = hand.IndexOf(instance);
        if (index < 0) return false;

        hand.RemoveAt(index);

        var view = cardViews[index];
        cardViews.RemoveAt(index);

        if (view != null)
        {
            Destroy(view.gameObject);
        }

        // 之後可以在這裡呼叫 ReorderHand()
        return true;
    }

    /// <summary>
    /// 清空整個手牌（例如戰鬥開始前、重開場）
    /// </summary>
    public void ClearHand()
    {
        hand.Clear();

        foreach (var v in cardViews)
        {
            if (v != null)
                Destroy(v.gameObject);
        }
        cardViews.Clear();
    }

    /// <summary>
    /// 之後可以放手牌排列（扇形、等距等）
    /// </summary>
    public void ReorderHand()
    {
        // 如果 handRoot 底下用的是 Horizontal/Vertical Layout Group，可以先不做任何事
        // 之後想要用 DOTween 做扇形排版，可以在這裡處理位置與旋轉
    }

    /// <summary>
    /// 用來找 UI View（如果你要從 CardView 回傳事件）
    /// </summary>
    public CardView GetViewForInstance(CardInstance instance)
    {
        int index = hand.IndexOf(instance);
        if (index < 0 || index >= cardViews.Count) return null;
        return cardViews[index];
    }
}