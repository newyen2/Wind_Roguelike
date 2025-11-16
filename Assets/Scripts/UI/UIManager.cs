using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject TextScoreParticle;
    public Transform Particle;
    Transform[,] pos_to_tile = new Transform[6, 6];

    // 展示分數用
    int now_score = 0;
    public TMP_Text score_text, max_score_text;

    void Awake()
    {
        Instance = this;
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach(var tile in tiles)
        {
            pos_to_tile[tile.tilePos.x, tile.tilePos.y] = tile.transform;
        }
    }
    
    void Start()
    {
        max_score_text.text = "需求 :" + StageManager.Instance.targetScore.ToString();
    }
    public void DisplayScoreText(int target)
    {
        StopCoroutine(ScoreText(target));
        StartCoroutine(ScoreText(target));
    }

    public void DisplayTextScoreParticle(int x, int y, int score)
    {
        GameObject gameObject = Instantiate(TextScoreParticle, pos_to_tile[x, y].position, Quaternion.identity, Particle);
        gameObject.GetComponent<TextScoreParticle>().setValue(score);
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
