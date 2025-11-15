using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GameObject[] buildingPrefabs;  // 4 種建築 Prefab
    GameObject selectedPrefab = null;
    public Transform buildParent;

    public GameObject[,] grid = new GameObject[4, 4]; // 用來記錄 4x4 建築

    void Awake()
    {
        Instance = this;
    }

    public void SelectBuilding(int id)
    {
        selectedPrefab = buildingPrefabs[id];
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
        if (grid[tilePos.x, tilePos.y] != null)
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

        // 記錄到陣列
        grid[tilePos.x, tilePos.y] = newBuilding;

        Debug.Log($"建築生成於 ({tilePos.x}, {tilePos.y})");

        // 放好後重置選取（看需求）
        selectedPrefab = null;
    }
}
