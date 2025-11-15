using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class GlobalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GlobalManager Instance { get; private set; }
    public static BuildManager InstanceBuildManager { get; private set; }
    public int groundSize = 4;
    public GameObject[,] grid;
    public List<GameObject> rewardBuild;
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
    }

    void Update()
    {
        
    }

    void InitializeStage()
    {
        StageManager.Instance.LoadStage();
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
