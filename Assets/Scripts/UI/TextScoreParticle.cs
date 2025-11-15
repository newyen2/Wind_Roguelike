using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScoreParticle : MonoBehaviour
{
    TMP_Text text;
    float exist_time = 2, now_time = 0;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(FloatUp());
    }

    public void setValue(int value)
    {
        text.text = value.ToString();
    }

    IEnumerator FloatUp()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        now_time = 0;

        while (now_time < exist_time)
        {
            now_time += Time.deltaTime;
            
            rectTransform.anchoredPosition += Vector2.up * 50 * Time.deltaTime;
            
            Color color = text.color;
            color.a = 1 - (now_time / exist_time);
            text.color = color;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}