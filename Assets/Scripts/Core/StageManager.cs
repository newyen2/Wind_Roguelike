using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Drawing;
using TMPro;
using Core;

public enum Direction
{
    E = 0,
    W = 1,
    S = 2,
    N = 3
}

public class StageManager : MonoBehaviour
{
    public int round;
    public int roundMax;
    public int score;
    public int targetScore;
    public WindSlot[,] windPosition;
    public WindSlot[,] nextWindPosition;
    public int step;

    public int delayDraw = 0;
    public int delaycost = 0;


    public bool is_round_going = false;
    public int powerPoint;
    public int maxPowerPoint;

    [SerializeField] TMP_Text energy;
    [SerializeField] TMP_Text count;

    public static StageManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int stage = GlobalManager.Instance.recordsCount;
        LoadStage(GlobalManager.Instance.records[stage].roundMax, GlobalManager.Instance.records[stage].targetScore, GlobalManager.Instance.records[stage].maxPowerPoint);
        windPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];
        nextWindPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];

        for (int i = 0; i < GlobalManager.Instance.groundSize + 2; i++)
        {
            for (int j = 0; j < GlobalManager.Instance.groundSize + 2; j++)
            {
                int inputDirection = -1;
                if (i == 0) {
                    inputDirection = (int)Direction.E;
                }
                if (i == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.W;
                }
                if (j == 0)
                {
                    inputDirection = (int)Direction.N;
                }
                if (j == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.S;
                }
                windPosition[i, j] = new WindSlot(i, j, 100, inputDirection);
                nextWindPosition[i, j] = new WindSlot(i, j, 100, inputDirection);
            }
        }
        //建立牌堆並抽牌
        DeckManager.Instance.StartBattle();
        renewBuild();
        for (int i = 0; i < GlobalManager.Instance.grid.GetLength(0); i++)
        {
            for (int j = 0; j < GlobalManager.Instance.grid.GetLength(1); j++)
            {
                 if (GlobalManager.Instance.grid[i, j] != null)
                {
                    int s = GlobalManager.Instance.grid[i, j].GetComponent<BuildingBase>().WhenStart(i, j);
                    score += s;
                    UIManager.Instance.DisplayTextScoreParticle(i, j, s);
                }
            }
        }
        
        UIManager.Instance.DisplayScoreText(StageManager.Instance.score);
        
    }

    public void addScore(int s)
    {
        score += s;
        UIManager.Instance.DisplayScoreText(StageManager.Instance.score);
    }

    public bool canPlay(CardInstance card, Vector2Int windTile)
    {
        if (card == null)
        {
            Debug.LogWarning("TryPlayCard: card is null");
            return false;
        }

        // 用 powerPoint 當唯一能量
        if (card.currentCost > powerPoint)
        {
            Debug.Log(
                $"能量不足，目前 {powerPoint}，需要 {card.currentCost}，無法打出 {card.data.displayName}"
            );
            return false;
        }

        bool needUp    = windTile.y == 0;
        bool needDown  = windTile.y == GlobalManager.Instance.groundSize + 1;
        bool needRight = windTile.x == 0;
        bool needLeft  = windTile.x == GlobalManager.Instance.groundSize + 1;

        // --- 用卡片的四個 bool 來比對 ---
        bool match =
            (needUp    && card.direction.up)    ||
            (needDown  && card.direction.down)  ||
            (needLeft  && card.direction.left)  ||
            (needRight && card.direction.right);

        if (!match)
        {
            Debug.Log($"Direction Error: Tile({windTile}) 需要方向 Up:{needUp} Down:{needDown} Left:{needLeft} Right:{needRight}，但卡不符合");
            return false;
        }
        
        return true;
    }
    

    public bool TryPlayCard(CardInstance card)//打牌檢測
    {
        if (card == null)
        {
            Debug.LogWarning("TryPlayCard: card is null");
            return false;
        }

        // 用 powerPoint 當唯一能量
        if (card.currentCost > powerPoint)
        {
            Debug.Log(
                $"能量不足，目前 {powerPoint}，需要 {card.currentCost}，無法打出 {card.data.displayName}"
            );
            return false;
        }

        foreach (CardEffectBase c in card.data.effects)
        {
            c.OnPlay(card , null);
        }

        // 扣能量
        powerPoint -= card.currentCost;
        


        // 決定丟去哪裡
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

    // Update is called once per frame
    void Update()
    {
        energy.text =""+ powerPoint;
        count.text = "" + (roundMax-round+1);
    }

    [Button]
    public void ExecuteRound()
    {
        if(is_round_going) return;
        is_round_going = true;
        StartCoroutine(NextRound());
    }

    public IEnumerator NextRound()
    {
        yield return StartCoroutine(CalcScore());
        foreach (Transform child in BuildWindManager.Instance.buildParentWind.transform)
        {
            Destroy(child.gameObject);
        }

        is_round_going = false;
        
        for (int i = 0; i < GlobalManager.Instance.grid.GetLength(0); i++)
        {
            for (int j = 0; j < GlobalManager.Instance.grid.GetLength(1); j++)
            {
                if (i == 0 || i == GlobalManager.Instance.grid.GetLength(0) - 1 || j == 0 || j == GlobalManager.Instance.grid.GetLength(1) - 1)
                {
                    GlobalManager.Instance.grid[i, j] = null;
                }
                else if(GlobalManager.Instance.grid[i, j] != null)
                {
                    int s = GlobalManager.Instance.grid[i, j].GetComponent<BuildingBase>().EndScore(i, j);
                    score += s;
                    UIManager.Instance.DisplayTextScoreParticle(i, j, s);
                }
            }
        }
        UIManager.Instance.DisplayScoreText(StageManager.Instance.score);


        if (score >= targetScore)
        {
            GameManager.Instance.total_score += score;
            Clear();
            yield break;
        }

        round++;

        if (round > roundMax)
        {
            Fail();
            yield break;
        }
        

        powerPoint = maxPowerPoint+delaycost;
        //棄牌, 重抽, 回點
        DeckManager.Instance.DiscardAllFromHand();//棄牌
        DeckManager.Instance.DrawCards(DeckManager.Instance.startingHandSize + delayDraw);//抽牌

        delaycost = 0;
        delayDraw = 0;
        renewBuild();

        
    }

    void renewBuild()
    {
        for (int i = 0; i < GlobalManager.Instance.grid.GetLength(0); i++)
        {
            for (int j = 0; j < GlobalManager.Instance.grid.GetLength(1); j++)
            {
                if (i == 0 || i == GlobalManager.Instance.grid.GetLength(0) - 1 || j == 0 || j == GlobalManager.Instance.grid.GetLength(1) - 1)
                {
                    GlobalManager.Instance.grid[i, j] = null;
                }
                else if (GlobalManager.Instance.grid[i, j] != null)
                {
                    GlobalManager.Instance.grid[i, j].GetComponent<BuildingBase>().Renew(round);
                }
            }
        }
    }

    [Button]
    public void LoadStage(int r, int t, int m)
    {
        round = 1;
        roundMax = r;
        score = 0;
        targetScore = t;
        maxPowerPoint = m;
        powerPoint = maxPowerPoint;
    }

    void Clear()
    {
        Debug.Log("Clear");
        GlobalManager.Instance.recordsCount += 1;
        if (GlobalManager.Instance.recordsCount == 10) GameManager.Instance.SwitchScene("GameWin");
        else GameManager.Instance.SwitchScene("Result");
    }
    void Fail()
    {
        Debug.Log("Fail");
        GameManager.Instance.SwitchScene("GameOver");

    }

    IEnumerator CalcScore()
    {
        bool able_wind = false;

        step = 0;
        for (; step < 10; step++)
        {
            able_wind = false;
            foreach (WindSlot windslot in windPosition)
            {
                windslot.Execute();
            }
            ApplyMove();
            yield return new WaitForSeconds(0.5f);

            // 當沒有 wind 可以跑的時候提早結束 CalcScore
            foreach (WindSlot windslot in windPosition)
            {
                foreach(Wind wind in windslot.windSlot)
                {
                    if (wind.isEnable)
                    {
                        able_wind = true;
                        break;
                    }
                }
                if(able_wind) break;
            }

            if(!able_wind) break;
        }
        
        newround();
    }


    [SerializeField] GameObject abaaba;
    void newround()
    {
        for (int i = 0; i < GlobalManager.Instance.groundSize + 2; i++)
        {
            for (int j = 0; j < GlobalManager.Instance.groundSize + 2; j++)
            {
                if ((i == 0 || (i == GlobalManager.Instance.groundSize + 1) || j == 0 || j == GlobalManager.Instance.groundSize + 1)
                    && GlobalManager.Instance.grid[i, j]!=null)
                {
                    abaaba = GlobalManager.Instance.gridobj[i, j];
                    GlobalManager.Instance.gridobj[i, j] = null;
                    GlobalManager.Instance.grid[i, j] = null;

                    Destroy(abaaba);

                }
            }
        }

    }

    [Button]
    public void AddWind(int x, int y, WindEffectBase[] windEffects, int power = 3, Wind wind = null)
    {
        Direction direction = Direction.N;
        if (x == 0)
        {
            direction = Direction.E;
        }
        if (x == GlobalManager.Instance.groundSize + 1)
        {
            direction = Direction.W;
        }
        if (y == 0)
        {
            direction = Direction.N;
        }
        if (y == GlobalManager.Instance.groundSize + 1)
        {
            direction = Direction.S;
        }
        wind = new Wind(direction, power);

        wind.effects = windEffects;
        windPosition[x, y].windSlot.Add(wind);
        Debug.Log($"Add Wind at ({x}, {y}) Direction: {direction}");
    }

    public void ApplyMove()
    {

        windPosition = nextWindPosition;
        nextWindPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];
        for (int i = 0; i < GlobalManager.Instance.groundSize + 2; i++)
        {
            for (int j = 0; j < GlobalManager.Instance.groundSize + 2; j++)
            {
                int inputDirection = -1;
                if (i == 0)
                {
                    inputDirection = (int)Direction.E;
                }
                if (i == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.W;
                }
                if (j == 0)
                {
                    inputDirection = (int)Direction.N;
                }
                if (j == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.S;
                }
                nextWindPosition[i, j] = new WindSlot(i, j, 100, inputDirection);
            }
        }
    }
}
