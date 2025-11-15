using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    int? currentSenderId = null;
    bool isBuilding = true;
    GameObject lastChoiceBuild;

    void Awake()
    {
        Instance = this;
    }

    public void SelectSender(int id, GameObject gameObject)
    {
        currentSenderId = id;
        if(gameObject.CompareTag("Building"))
        {
            BuildManager.Instance.SelectBuilding(id);
            lastChoiceBuild = gameObject;
            isBuilding = true;
        }
        if(gameObject.CompareTag("Wind"))
        {
            BuildWindManager.Instance.SelectBuilding(id);
            lastChoiceBuild = gameObject;
            isBuilding = false;
        }
        Debug.Log($"Sender 已紀錄: {id}");
    }

    public bool TryGetSender(out int senderId, GameObject gameObject)
    {
        if (currentSenderId.HasValue)
        {
            senderId = currentSenderId.Value;
            currentSenderId = null; // 用完清空（可依需求調整）
            if(gameObject.CompareTag("Tile") && isBuilding)
            {
                BuildManager.Instance.TryBuild(gameObject.GetComponent<Tile>().tilePos, gameObject.transform);
                Destroy(lastChoiceBuild);
            }
            if(gameObject.CompareTag("WindTile") && !isBuilding)
            {
                BuildWindManager.Instance.TryBuild(gameObject.GetComponent<WindTile>().tilePos, gameObject.transform);
                Destroy(lastChoiceBuild);
            }
            return true;
        }
        senderId = -1;
        return false;
    }
}
