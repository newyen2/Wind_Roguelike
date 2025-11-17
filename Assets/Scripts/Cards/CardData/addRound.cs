using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "addRound", menuName = "Card/effect/addRound")]
public class addRound : CardEffectBase
{
    public int rounds;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        StageManager.Instance.roundMax += rounds;

    }
}
