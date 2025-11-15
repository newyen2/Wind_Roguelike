using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int tilePos;

    void Start()
    {
        this.name = $"[{tilePos.x}, {tilePos.y}]";
    }
}