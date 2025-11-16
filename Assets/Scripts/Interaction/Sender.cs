using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sender : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler //這個有空再來做
{
    public int senderId;
    public bool is_set = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.Play("click");
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

        if (hitObj != null && hitObj.TryGetComponent<Tile>(out var tile) && this.CompareTag("Building"))
        {
            Debug.Log("放在 Tile：" + tile.tilePos);
            if(BuildManager.Instance.canbuild(tile.tilePos, tile.transform))
            {
                BuildManager.Instance.TryBuild(tile.tilePos, tile.transform);
                is_set = true;
                Destroy(this.gameObject);
                return;

            }
        }

        if (hitObj != null && hitObj.TryGetComponent<WindTile>(out var windTile) && this.CompareTag("Wind"))
        {
            Debug.Log("放在 Tile：" + windTile.tilePos);
            if(StageManager.Instance.canPlay(this.gameObject.GetComponent<CardView>().instance , windTile.tilePos))
            {
                if(BuildWindManager.Instance.canBuild(windTile.tilePos, windTile.transform))
                {
                    StageManager.Instance.TryPlayCard(this.gameObject.GetComponent<CardView>().instance);
                    BuildWindManager.Instance.TryBuild(windTile.tilePos, windTile.transform);
                    is_set = true;
                    Destroy(this.gameObject);
                    return;

                }

            }
        }

        Debug.Log("沒放在 Tile 上");
        if(this.CompareTag("Building")){
            transform.SetParent(BuildManager.Instance.nowBuilding);
            BuildManager.Instance.nowBuilding.GetComponent<VerticalLayoutGroup>().spacing += 1;
            BuildManager.Instance.nowBuilding.GetComponent<VerticalLayoutGroup>().spacing -= 1;
        }
        else {
            transform.SetParent(BuildWindManager.Instance.nowBuilding);
            BuildWindManager.Instance.nowBuilding.GetComponent<HorizontalLayoutGroup>().spacing += 1;
            BuildWindManager.Instance.nowBuilding.GetComponent<HorizontalLayoutGroup>().spacing -= 1;
        }
    }
}
