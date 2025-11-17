using UnityEngine;
using UnityEngine.UI;

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
            if (gameObject.CompareTag("Tile") && isBuilding)
            {
                if (BuildManager.Instance.canbuild(gameObject.GetComponent<Tile>().tilePos, gameObject.transform))
                {
                    BuildManager.Instance.TryBuild(gameObject.GetComponent<Tile>().tilePos, gameObject.transform);
                    Destroy(lastChoiceBuild);
                    currentSenderId = null; // 用完清空（可依需求調整）
                }
            }
            if (gameObject.CompareTag("WindTile") && !isBuilding)
            {
                if (lastChoiceBuild.GetComponent<CardView>() == null || gameObject.GetComponent<WindTile>() == null) return false;
                if (StageManager.Instance.canPlay(lastChoiceBuild.GetComponent<CardView>().instance,gameObject.GetComponent<WindTile>().tilePos))
                {
                    WindTile windtile_tmp = gameObject.GetComponent<WindTile>();
                    if (BuildWindManager.Instance.canBuild(windtile_tmp.tilePos, windtile_tmp.transform))
                    {
                        StageManager.Instance.TryPlayCard(lastChoiceBuild.GetComponent<CardView>().instance);
                        BuildWindManager.Instance.TryBuild(windtile_tmp.tilePos, windtile_tmp.transform, lastChoiceBuild.GetComponent<CardView>());
                        Destroy(lastChoiceBuild); 
                        currentSenderId = null; // 用完清空（可依需求調整）
                    }
                    Debug.Log("沒放在 Tile 上");

                }
                else
                {
                    print("no cost");
                }
            }
            return true;
        }
        senderId = -1;
        return false;
    }
}
