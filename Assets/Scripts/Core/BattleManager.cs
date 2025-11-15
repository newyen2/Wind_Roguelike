using UnityEngine;
using Sirenix.OdinInspector; // 如果你想用按鈕 debug 用，不想用可以拿掉

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    [Header("回合設定")]
    public int energyPerTurn = 3;     // 每回合補多少能量
    public int maxEnergy = 3;         // 能量上限
    public int startingHandSizeOverride = -1; // 若 >=0 則覆蓋 DeckManager 的 startingHandSize

    [ShowInInspector, ReadOnly]
    public int CurrentEnergy { get; private set; }

    [ShowInInspector, ReadOnly]
    public int TurnIndex { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // 進入 Stage 場景後，自動開始一場戰鬥
        StartBattle();
    }

    /// <summary>
    /// 一場戰鬥的開始：建立牌堆 + 抽起始手牌 + 開啟第一回合
    /// </summary>
    public void StartBattle()
    {
        TurnIndex = 0;
        CurrentEnergy = 0;

        // 建立牌堆並抽起始手牌
        DeckManager.Instance.StartBattle();

        // 如果你想讓 BattleManager 控制起始手牌數，也可以改成：
        if (startingHandSizeOverride >= 0)
        {
            HandManager.Instance.ClearHand();
            DeckManager.Instance.DrawCards(startingHandSizeOverride);
        }

        StartPlayerTurn();
    }

    /// <summary>
    /// 開始玩家的新回合
    /// </summary>
    public void StartPlayerTurn()
    {
        TurnIndex++;

        // 回合開始補能量
        CurrentEnergy = Mathf.Min(CurrentEnergy + energyPerTurn, maxEnergy);
        Debug.Log($"Turn {TurnIndex} start. Energy: {CurrentEnergy}");

        // 如果你想做「每回合抽 N 張」也可以在這裡做：
        // DeckManager.Instance.DrawCards(cardsPerTurn);
    }

    /// <summary>
    /// 回合結束：丟棄全部手牌，然後抽新手牌，再開始下一回合
    /// </summary>
    [Button] // Odin 按鈕，方便在編輯模式測試
    public void EndPlayerTurn()
    {
        DeckManager.Instance.DiscardAllFromHand();//棄牌
        DeckManager.Instance.DrawCards(DeckManager.Instance.startingHandSize);//抽牌
        StartPlayerTurn();//開新回合
    }

    /// <summary>
    /// 試圖打出一張牌：檢查能量 → 執行效果 → 丟到棄牌 / 消耗堆
    /// </summary>
    public bool TryPlayCard(CardInstance card)
    {
        if (card == null)
        {
            Debug.LogWarning("TryPlayCard called with null card.");
            return false;
        }

        // 1. 能量檢查
        if (card.currentCost > CurrentEnergy)
        {
            Debug.Log("Energy not enough!");
            return false;
        }

        // 2. 扣費
        CurrentEnergy -= card.currentCost;
        Debug.Log($"Play card: {card.data.displayName}, cost {card.currentCost}, energy now {CurrentEnergy}");

        // 3. 執行卡片效果（之後你可以掛 CardEffectBase）
        // Example:
        // foreach (var effect in card.data.effects)
        // {
        //     effect.Execute(...);
        // }

        // 4. 丟到棄牌 / 消耗堆
        if (card.isExhausted)
        {
            DeckManager.Instance.ExhaustFromHand(card);
        }
        else
        {
            DeckManager.Instance.DiscardFromHand(card);
        }

        return true;
    }
}