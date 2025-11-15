using UnityEngine;
using UnityEngine.EventSystems;

public class Sender : MonoBehaviour, IPointerClickHandler
{
    public int senderId;

    public void OnPointerClick(PointerEventData eventData)
    {
        InteractionManager.Instance.SelectSender(senderId, gameObject);
    }
}
