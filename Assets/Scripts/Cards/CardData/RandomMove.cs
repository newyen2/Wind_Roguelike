using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomMove", menuName = "Card/windEffect/RandomMove")]
public class RandomMove : WindEffectBase
{
    public override void OnAfterMove(Wind w)
    {
        Debug.Log($"Random Move!{StageManager.Instance.step}, {w.direction}");
        if (StageManager.Instance.step >= 1)
        {
            w.direction = (Direction)Random.Range(0, 4);
        }
        Debug.Log($"New Direction: {w.direction}");
    }
}
