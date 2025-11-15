using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject[,] A;


    public int powerPoint;
    public int maxPowerPoint;

    public static StageManager Instance { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        windPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextRound()
    {
        CalcScore();
        windPosition = nextWindPosition;
        nextWindPosition = new WindSlot[GlobalManager.Instance.groundSize + 2, GlobalManager.Instance.groundSize + 2];

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

    void LoadStage()
    {
        // roundMax = 3;
        // targetScore = 1000;
    }

    void Clear()
    {

    }
    void Fail()
    {
        
    }

    void CalcScore()
    {
        foreach (WindSlot windslot in windPosition)
        {
            windslot.Execute();
        }

    }
}
