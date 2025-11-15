using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildWindManager : MonoBehaviour
{
    public static BuildWindManager Instance;

    public GameObject[] buildingWindPrefabs;  
    GameObject selectedPrefab = null;
    public Transform buildParent, nowBuilding;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Tile[] tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach(var tile in tiles)
        {
            GameObject tmp = GlobalManager.Instance.grid[tile.tilePos.x, tile.tilePos.y];
            if(tmp != null)
            {
                GameObject newBuilding = Instantiate(
                    tmp,
                    tile.transform.position,
                    Quaternion.identity,
                    buildParent
                );
                newBuilding.GetComponent<Sender>().is_set = true;
                Debug.Log($"[{tile.tilePos.x}, {tile.tilePos.y}] has building {newBuilding.GetComponent<Sender>().senderId}");
            }
        }

        foreach(var build in GlobalManager.Instance.rewardBuild)
        {
            //Instantiate(build, nowBuilding);
        }
    }

    public void SelectBuilding(int id)
    {
        selectedPrefab = buildingWindPrefabs[id];
        Debug.Log("選擇建築 ID = " + id);
    }

    public void TryBuild(Vector2Int tilePos, Transform tileTransform)
    {
        if (selectedPrefab == null)
        {
            Debug.Log("尚未選擇建築");
            return;
        }

        // 如果該位置已經有建築
        if (GlobalManager.Instance.grid[tilePos.x, tilePos.y] != null)
        {
            Debug.Log("這格已經有建築");
            return;
        }

        // 在 tile 上生成建築
        GameObject newBuilding = Instantiate(
            selectedPrefab,
            tileTransform.position,
            Quaternion.identity,
            buildParent
        );
        newBuilding.GetComponent<Sender>().is_set = true;

        // 記錄到陣列
        StageManager.Instance.AddWind(tilePos.x, tilePos.y);

        Debug.Log($"建築生成於 ({tilePos.x}, {tilePos.y})");

        // 放好後重置選取（看需求）
        selectedPrefab = null;
    }
}
