using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//管理：牌堆 / 棄牌堆 / 消耗堆, 提供：抽牌 / 丟牌 / 消耗 / 洗牌等 API
public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }
    [Header("起始套牌（設計時設定）")]
    public List<CardData> startingDeck = new List<CardData>();
    [Header("開局起始手牌數量")]
    public int startingHandSize = 5;
    private readonly List<CardInstance> drawPile    = new List<CardInstance>();
    private readonly List<CardInstance> discardPile = new List<CardInstance>();
    private readonly List<CardInstance> exhaustPile = new List<CardInstance>();
    public IReadOnlyList<CardInstance> DrawPile    => drawPile;
    public IReadOnlyList<CardInstance> DiscardPile => discardPile;
    public IReadOnlyList<CardInstance> ExhaustPile => exhaustPile;
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
    /// 一場戰鬥開始時呼叫，生成實際牌堆並洗牌＋抽起始手牌
    /// </summary>
    public void StartBattle()
    {
        drawPile.Clear();
        discardPile.Clear();
        exhaustPile.Clear();

        // 由 ScriptableObject 建戰鬥實例
        foreach (var cardData in startingDeck)
        {
            var instance = new CardInstance(cardData);
            drawPile.Add(instance);
        }

        Shuffle(drawPile);

        // 清空手牌，抽起始手牌
        HandManager.Instance.ClearHand();
        DrawCards(startingHandSize);
        //Debug_DrawDummyHand();
    }

    /// <summary>
    /// 抽 n 張牌進手牌
    /// </summary>
    public void DrawCards(int count)
    {
        Debug.Log($"抽牌,抽{count}張");
        for (int i = 0; i < count; i++)
        {
            // 如果抽牌堆沒牌了，試著把棄牌堆洗回來
            if (drawPile.Count == 0)
            {
                RefillDrawPileFromDiscard();
            }

            if (drawPile.Count == 0)
            {
                // 棄牌堆也沒有 → 完全沒牌可抽
                return;
            }

            var topCard = drawPile[0];
            drawPile.RemoveAt(0);

            HandManager.Instance.AddCardToHand(topCard);
            //生幾個Instance來做看看
            
            // 之後可以在這裡加：類似小丑牌的系統/遺物觸發邏輯JokerSystem.OnCardDrawn(topCard);
        }
        //這裡可以改牌堆數據
    }

    /// <summary>
    /// 將指定手牌丟到棄牌堆
    /// </summary>
    public void DiscardFromHand(CardInstance card)
    {
        bool removed = HandManager.Instance.RemoveCardFromHand(card);
        if (!removed) return;

        discardPile.Add(card);
        // 之後可以在這裡掛 OnDiscard Hooks 棄牌時觸發效果
        
    }

    /// <summary>
    /// 將指定手牌消耗（從本場戰鬥中移除）
    /// </summary>
    public void ExhaustFromHand(CardInstance card)
    {
        bool removed = HandManager.Instance.RemoveCardFromHand(card);
        if (!removed) return;

        card.isExhausted = true;
        exhaustPile.Add(card);
    }

    /// <summary>
    /// 將棄牌堆洗回抽牌堆
    /// </summary>
    private void RefillDrawPileFromDiscard()
    {
        if (discardPile.Count == 0) return;

        drawPile.AddRange(discardPile);
        discardPile.Clear();

        Shuffle(drawPile);

        // 之後可以在這裡觸發「洗牌時觸發的小丑牌 / 遺物」
        // JokerSystem.OnDeckShuffled();
    }

    /// <summary>
    /// Fisher-Yates 洗牌
    /// </summary>
    public void Shuffle(List<CardInstance> pile)
    {
        for (int i = 0; i < pile.Count; i++)
        {
            int randomIndex = Random.Range(i, pile.Count);
            var temp = pile[i];
            pile[i] = pile[randomIndex];
            pile[randomIndex] = temp;
        }
    }

    /// <summary>
    /// 外部如果要把某張牌加入抽牌堆（例如產生特殊牌）
    /// </summary>
    public void AddCardToDrawPile(CardInstance card, bool shuffleAfterAdd = false)
    {
        drawPile.Add(card);
        if (shuffleAfterAdd)
        {
            Shuffle(drawPile);
        }
    }

    /// <summary>
    /// 外部如果要把某張牌直接加入棄牌堆（例如被棄置的 token）
    /// </summary>
    public void AddCardToDiscardPile(CardInstance card)
    {
        discardPile.Add(card);
    }
    public void DiscardAllFromHand()
    {
        // 戰鬥規則：把現在所有手牌視為「要丟棄的牌」
        // 棄光手牌
        Debug.Log("棄光手牌");
        var snapshot = new List<CardInstance>(HandManager.Instance.CardsInHand);
        foreach (var card in snapshot)
        {
            DiscardFromHand(card);   // 這裡會順便叫 HandManager.RemoveCardFromHand + 丟到棄牌堆
        }
    }
    
    public void Debug_DrawDummyHand()
    {
        int dummyHandSizeForTest = 5;
        HandManager.Instance.SpawnDummyCardsForUITest(dummyHandSizeForTest);
        Debug.Log("生成DummyCard");
    }
}