using UnityEngine;
using UnityEngine.EventSystems;

public class Receiver : MonoBehaviour, IPointerClickHandler
{
    public int receiverValue; // 最後被塞進來的 senderId

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InteractionManager.Instance.TryGetSender(out int id, gameObject))
        {
            receiverValue = id;
            
            Debug.Log($"Receiver 收到 senderId: {id}");
        }
        else
        {
            Debug.Log("目前沒有 Sender 可供接收");
        }
    }
}
