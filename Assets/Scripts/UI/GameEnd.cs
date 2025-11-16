using System.Collections;
using Core;
using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] Transform Title;
    [SerializeField] Transform Lore;
    [SerializeField] Transform ReturnButton;

    void Start()
    {
        Lore.GetComponent<TMP_Text>().text = "本局分數 : " + GameManager.Instance.total_score.ToString();
        // 預先隱藏物件
        if (Title) Title.gameObject.SetActive(false);
        if (Lore) Lore.gameObject.SetActive(false);
        if (ReturnButton) ReturnButton.gameObject.SetActive(false);

        StartCoroutine(PlayEndSequence());
    }

    IEnumerator PlayEndSequence()
    {
        // 依序放大每個物件：先顯示，再從 0->1 放大 1 秒，放大完等待 1 秒
        if (Title) yield return StartCoroutine(ScaleUpAndPause(Title));
        if (Lore) yield return StartCoroutine(ScaleUpAndPause(Lore));
        if (ReturnButton) yield return StartCoroutine(ScaleUpAndPause(ReturnButton));
    }

    IEnumerator ScaleUpAndPause(Transform t)
    {
        t.gameObject.SetActive(true);
        t.localScale = Vector3.zero;

        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float p = Mathf.Clamp01(elapsed / duration);
            t.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);
            yield return null;
        }
        t.localScale = Vector3.one;

        // 放大完成後再等 1 秒
        yield return new WaitForSeconds(0.5f);
    }
}