using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortUILayer : MonoBehaviour
{
    // Start 後等待 0.1 秒再整理
    void Start()
    {
        StartCoroutine(SortChildrenByY());
    }

    // 讀取此 UI（此物件的所有直接子物件），依 RectTransform 的 y 排序
    // y 越小的牌會被放到最前面（SiblingIndex 越大，渲染越靠前）
    public IEnumerator SortChildrenByY()
    {
        yield return null;  
        yield return null;  
        var list = new List<RectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var rt = child.GetComponent<RectTransform>();
            if (rt != null)
                list.Add(rt);
        }

        // 依 position.y 升冪排序（y 小排後面）
        list.Sort((a, b) => a.position.y.CompareTo(b.position.y));

        int n = list.Count;
        // 將 y 越小的設為越大的 sibling index（渲染在最前）
        for (int i = 0; i < n; i++)
        {
            list[i].SetSiblingIndex(n - 1 - i);
        }
    }
}