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


    // Start is called before the first frame update
    void Start()
    {
        windPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];
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
                windPosition[i, j] = new WindSlot(i, j, 1, inputDirection);
            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void NextRound()
    {
        CalcScore();

        if (score >= targetScore)
        {
            Clear();
            return;
        }

        round++;

        if (round > roundMax)
        {
            Fail();
            return;
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

    void CalcScore()
    {
        foreach (WindSlot windslot in windPosition)
        {
            windslot.Execute();
        }

    }
}
