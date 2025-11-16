using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
    float exist_time = 0.5f, now_time = 0;
    Direction direction;

    void Awake()
    {
        StartCoroutine(FloatUp());
    }

    public void setDir(Direction dir)
    {
        direction = dir;
        transform.rotation = Quaternion.Euler(0, 0, GetDirectionAngle(dir));
    }

    private float GetDirectionAngle(Direction dir)
    {
        return dir switch
        {
            Direction.N => 0f,
            Direction.E => 90f,
            Direction.S => 180f,
            Direction.W => 270f,
            _ => 0f
        };
    }

    IEnumerator FloatUp()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        now_time = 0;
        yield return null;
        yield return null;
        Vector2 moveDirection = GetMoveDirection();
        var img = GetComponent<UnityEngine.UI.Image>();
        Color originalColor = img != null ? img.color : Color.white;

        while (now_time < exist_time)
        {
            now_time += Time.deltaTime;

            rectTransform.anchoredPosition += moveDirection * 50 * Time.deltaTime;

            if (img != null)
            {
            float t = Mathf.Clamp01(now_time / exist_time);
            Color c = originalColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            img.color = c;
            }

            yield return null;
        }

        if (img != null)
        {
            Color c = originalColor;
            c.a = 0f;
            img.color = c;
        }
        Destroy(gameObject);
    }

    Vector2 GetMoveDirection()
    {
        return direction switch
        {
            Direction.N => Vector2.up,
            Direction.S => Vector2.down,
            Direction.W => Vector2.left,
            Direction.E => Vector2.right,
            _ => Vector2.zero
        };
    }

}