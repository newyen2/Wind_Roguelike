using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColliderDetect : MonoBehaviour
{
    // 這個有空再搞
    public static ColliderDetect Instance { get; private set; }
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public GameObject GetTileUnderMouse()
    {
        PointerEventData data = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new();
        raycaster.Raycast(data, results);

        foreach (var r in results)
        {
            if (r.gameObject.TryGetComponent<Tile>(out var tile))
                return r.gameObject;
            if (r.gameObject.TryGetComponent<WindTile>(out var windTile))
                return r.gameObject;
        }

        return null;
    }
}
