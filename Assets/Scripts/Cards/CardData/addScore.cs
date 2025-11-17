using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "addscore", menuName = "Card/effect/addi")]
public class addScore : CardEffectBase
{
    public int addi;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        StageManager.Instance.addScore(addi);
    }
}
