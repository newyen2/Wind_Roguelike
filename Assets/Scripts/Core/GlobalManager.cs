using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        grid = new GameObject[groundSize, groundSize];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
