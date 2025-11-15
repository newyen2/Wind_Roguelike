using System.Collections.Generic;
using UnityEngine;

public class AdjustScreenScale : MonoBehaviour
{
    public List<Transform> ScaleChange;
    private float scaleFactor = 0;
    private const float baseWidth = 1920, baseHeight = 1080;

    void Update()
    {
        if (Mathf.Abs(scaleFactor - GetCurrentScale()) > 0.001f) AdjustUIScale();
    }

    void AdjustUIScale()
    {
        scaleFactor = GetCurrentScale();
        foreach (Transform t in ScaleChange)
        {
            if (t != null)
                t.localScale = Vector3.one * scaleFactor;
        }
    }

    public float GetCurrentScale()
    {
        float nowX = Screen.width;
        float nowY = Screen.height;
        return Mathf.Min(nowX / baseWidth, nowY / baseHeight);
    }
}