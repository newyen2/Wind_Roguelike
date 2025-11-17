using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "draw_card", menuName = "Card/effect/draw")]
public class drawCard : CardEffectBase
{
    public int draw;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        StageManager.Instance.delayDraw += draw;
    }
}
