using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stronger", menuName = "Card/windEffect/Stronger")]
public class Stronger : WindEffectBase
{
    public override void OnAfterMove(Wind w)
    {
        w.power += 1;
    }
}
