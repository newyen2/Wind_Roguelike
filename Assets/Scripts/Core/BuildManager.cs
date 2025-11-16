using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
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
                GlobalManager.Instance.gridobj[tile.tilePos.x, tile.tilePos.y] = newBuilding;
                Debug.Log($"[{tile.tilePos.x}, {tile.tilePos.y}] has building {newBuilding.GetComponent<Sender>().senderId} at {tile.transform.position}");
            }
        }

        foreach(var build in GlobalManager.Instance.rewardBuild)
        {
            Instantiate(build, nowBuilding);
        }
    }

    public void SelectBuilding(int id)
    {
        selectedPrefab = GlobalManager.Instance.buildingPrefabs[id];
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
        buildParent.GetComponent<SortUILayer>().StartCoroutine(buildParent.GetComponent<SortUILayer>().SortChildrenByY());

        print(tileTransform.position);

        // 記錄到陣列
        GlobalManager.Instance.grid[tilePos.x, tilePos.y] = selectedPrefab;

        Debug.Log($"建築生成於 ({tilePos.x}, {tilePos.y})");

        // 放好後重置選取（看需求）
        selectedPrefab = null;
    }

    // 這個是測試用的，之後幫我刪掉
    public void GoToReward()
    {
        GameManager.Instance.SwitchScene("Result");
    }

    public void GoToStage()
    {
        GameManager.Instance.SwitchScene("Stage_test");
    }
}
