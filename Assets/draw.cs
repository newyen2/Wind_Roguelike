using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draw : MonoBehaviour
{
    public static draw Instance { get; private set; }
    public bool finish;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> rewards = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        finish = false;
        List<GameObject> rewards = new List<GameObject>();
        prepare();
    }

    void prepare()
    {
        GameObject[] gameObjects = GlobalManager.Instance.buildingPrefabs;
        // collect only prefabs that are allowed by recordsCount
        List<GameObject> available = new List<GameObject>();
        List<int> weights = new List<int>();
        foreach (GameObject gb in gameObjects)
        { 
            if (gb.GetComponent<BuildingBase>().max_round < GlobalManager.Instance.recordsCount) continue;
            if (gb.GetComponent<BuildingBase>().min_round > GlobalManager.Instance.recordsCount) continue;
            available.Add(gb);
            weights.Add(gb.GetComponent<BuildingBase>().weight);
        }
        List<int> picked_indices = WeightedRandomPick(3, weights);
        foreach (int idx in picked_indices)
        {
            rewards.Add(available[idx]);
        }
        finish = true;
    }

    List<int> WeightedRandomPick(int count, List<int> weights)
    {
        List<int> results = new List<int>();
        List<int> pool = new List<int>();

        // 初始 pool：0,1,2,3...
        for (int i = 0; i < weights.Count; i++)
            pool.Add(i);

        for (int k = 0; k < count; k++)
        {
            // 計算 pool 裡剩下的總權重
            float total = 0;
            foreach (int idx in pool)
                total += weights[idx];

            // 隨機一個範圍
            float r = UnityEngine.Random.value * total;

            // 找到該權重對應的 index
            float sum = 0;
            int chosen = -1;

            foreach (int idx in pool)
            {
                sum += weights[idx];
                if (r <= sum)
                {
                    chosen = idx;
                    break;
                }
            }

            results.Add(chosen);
            pool.Remove(chosen); // 不重複 → 從 pool 移除
        }

        return results;
    }

}
