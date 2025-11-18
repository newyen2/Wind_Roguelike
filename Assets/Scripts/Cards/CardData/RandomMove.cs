using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomMove", menuName = "Card/windEffect")]
public class RandomMove : WindEffectBase
{
    public override void OnAfterMove(Wind w)
    {
        if (StageManager.Instance.step > 2)
        {
            w.direction = (Direction)Random.Range(0, 4);
        }
    }
}
