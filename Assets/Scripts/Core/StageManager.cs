using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Drawing;
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

    public bool is_round_going = false;
    public int powerPoint;
    public int maxPowerPoint;

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

    }

    // Update is called once per frame
    void Update()
    {
        
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
            }
        }


        if (score >= targetScore)
        {
            Clear();
            yield break;
        }

        round++;

        if (round > roundMax)
        {
            Fail();
            yield break;
        }

        powerPoint = maxPowerPoint;
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
        GameManager.Instance.SwitchScene("Result");
    }
    void Fail()
    {
        Debug.Log("Fail");
        GameManager.Instance.SwitchScene("GameOver");

    }

    IEnumerator CalcScore()
    {
        for (int i = 0; i < 10; i++)
        {
            foreach (WindSlot windslot in windPosition)
            {
                windslot.Execute();
            }
            ApplyMove();

            yield return new WaitForSeconds(0.5f);
        }

    }

    [Button]
    public void AddWind(int x, int y, Direction dir = Direction.N, int power = 3, Wind wind = null)
    {
        Direction direction = dir;
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
