using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Profiling;
using UnityEngine.SocialPlatforms.Impl;


public class GlobalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalManager Instance { get; private set; }
    public static BuildManager InstanceBuildManager { get; private set; }
    public int groundSize = 4;
    public GameObject[,] grid;
    public GameObject[,] gridobj;
    public GameObject[] buildingPrefabs;
    public CardData[] windPrefabs;
    public List<GameObject> rewardBuild;

    public List<StageRecord> records = new List<StageRecord>()
        {
            new StageRecord(3, 10, 3),
            new StageRecord(3, 20, 3),
            new StageRecord(3, 30, 3),
            new StageRecord(3, 50, 3),
            new StageRecord(3, 80, 3),
            new StageRecord(3, 120, 3),
            new StageRecord(3, 180, 3),
            new StageRecord(3, 220, 3),
            new StageRecord(3, 300, 3),
            new StageRecord(3, 500, 3),
        };
    public int recordsCount = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        grid = new GameObject[groundSize + 2, groundSize + 2];
        gridobj = new GameObject[groundSize + 2, groundSize + 2];
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void StartStage() {
        GameManager.Instance.SwitchScene("Stage");
    }

    [SerializeField]
    private GameObject[] resultGrid;
    [Button]
    public GameObject[] GetGrid(int x)
    {
        int height = grid.GetLength(1);
        GameObject[] result = new GameObject[height];

        for (int y = 0; y < height; y++)
            result[y] = grid[x, y];

        return result;
    }
}
public class StageRecord
{
    public int roundMax;
    public int targetScore;
    public int maxPowerPoint;

    public StageRecord(int roundMax, int targetScore, int maxPowerPoint)
    {
        this.roundMax = roundMax;
        this.targetScore = targetScore;
        this.maxPowerPoint = maxPowerPoint;
    }
}