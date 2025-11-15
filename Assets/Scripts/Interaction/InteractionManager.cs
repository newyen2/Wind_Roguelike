using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    int? currentSenderId = null;

    void Awake()
    {
        Instance = this;
    }

    public void SelectSender(int id, GameObject gameObject)
    {
        currentSenderId = id;
        if(gameObject.CompareTag("Building")) BuildManager.Instance.SelectBuilding(id);
        Debug.Log($"Sender 已紀錄: {id}");
    }

    public bool TryGetSender(out int senderId, GameObject gameObject)
    {
        if (currentSenderId.HasValue)
        {
            senderId = currentSenderId.Value;
            currentSenderId = null; // 用完清空（可依需求調整）
            if(gameObject.CompareTag("Tile")) BuildManager.Instance.TryBuild(gameObject.GetComponent<Tile>().tilePos, gameObject.transform);
            return true;
        }
        senderId = -1;
        return false;
    }
}
