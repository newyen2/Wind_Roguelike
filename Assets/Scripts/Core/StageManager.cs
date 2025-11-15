using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.Drawing;

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
        windPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];
        nextWindPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];

        for (int i = 0; i < GlobalManager.Instance.groundSize + 2; i++)
        {
            for (int j = 0; j < GlobalManager.Instance.groundSize + 2; j++)
            {
                int inputDirection = -1;
                if (i == 0) {
                    inputDirection = (int)Direction.N;
                }
                if (i == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.S;
                }
                if (j == 0)
                {
                    inputDirection = (int)Direction.E;
                }
                if (j == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.W;
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
        StartCoroutine(NextRound());
    }

    public IEnumerator NextRound()
    {
        yield return StartCoroutine(CalcScore());

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
    public void LoadStage()
    {
        // 實際上這邊是要讀Script或JSON的
        round = 1;
        roundMax = 3;
        score = 0;
        targetScore = 100;
        maxPowerPoint = 3;
        powerPoint = maxPowerPoint;
    }

    void Clear()
    {
        Debug.Log("Clear");
    }
    void Fail()
    {
        Debug.Log("Fail");
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
    void AddWind(int x, int y, Direction dir, Wind wind = null)
    {
        Direction direction = dir;
        if (x == 0)
        {
            direction = Direction.N;
        }
        if (x == GlobalManager.Instance.groundSize + 1)
        {
            direction = Direction.S;
        }
        if (y == 0)
        {
            direction = Direction.E;
        }
        if (y == GlobalManager.Instance.groundSize + 1)
        {
            direction = Direction.W;
        }
        wind = new Wind(direction);
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
                    inputDirection = (int)Direction.N;
                }
                if (i == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.S;
                }
                if (j == 0)
                {
                    inputDirection = (int)Direction.E;
                }
                if (j == GlobalManager.Instance.groundSize + 1)
                {
                    inputDirection = (int)Direction.W;
                }
                nextWindPosition[i, j] = new WindSlot(i, j, 100, inputDirection);
            }
        }
    }
}
