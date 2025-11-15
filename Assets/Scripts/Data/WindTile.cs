using UnityEngine;

public class WindTile : MonoBehaviour
{
    public Vector2Int tilePos;

    void Start()
    {
        this.name = $"[{tilePos.x}, {tilePos.y}]";
    }
}