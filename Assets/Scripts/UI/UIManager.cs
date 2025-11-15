using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // 展示分數用
    int now_score = 0;
    public TMP_Text score_text;

    void Awake()
    {
        Instance = this;
    }

    public void DisplayScoreText(int target)
    {
        StopCoroutine(ScoreText(target));
        StartCoroutine(ScoreText(target));
    }

    IEnumerator ScoreText(int target)
    {
        int tmp_score = now_score;
        while(now_score < target)
        {
            now_score += math.max(1, (int)(target - tmp_score) / 20);
            score_text.text = now_score.ToString();
            yield return null;
            yield return null;
            yield return null;
        }
        now_score = target;
        score_text.text = now_score.ToString();
        yield return null;
    }
}
