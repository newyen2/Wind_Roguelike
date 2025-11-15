using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sender : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler //這個有空再來做
{
    public int senderId;
    public bool is_set = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!is_set) InteractionManager.Instance.SelectSender(senderId, gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(is_set) return;
        transform.SetParent(transform.parent);
        InteractionManager.Instance.SelectSender(senderId, gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (is_set) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (is_set) return;

        var hitObj = ColliderDetect.Instance?.GetTileUnderMouse();

        if (hitObj != null && hitObj.TryGetComponent<Tile>(out var tile))
        {
            Debug.Log("放在 Tile：" + tile.tilePos);
            BuildManager.Instance.TryBuild(tile.tilePos, tile.transform);
            is_set = true;
        }
        else
        {
            Debug.Log("沒放在 Tile 上");
            transform.SetParent(BuildManager.Instance.nowBuilding);
            BuildManager.Instance.nowBuilding.GetComponent<VerticalLayoutGroup>().spacing += 1;
            BuildManager.Instance.nowBuilding.GetComponent<VerticalLayoutGroup>().spacing -= 1;
        }
    }
}
