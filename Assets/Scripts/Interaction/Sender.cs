using UnityEngine;
using UnityEngine.EventSystems;

public class Sender : MonoBehaviour, IPointerClickHandler
{
    public int senderId;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(transform.parent.name == "NowBuilding") InteractionManager.Instance.SelectSender(senderId, gameObject);
    }
}
