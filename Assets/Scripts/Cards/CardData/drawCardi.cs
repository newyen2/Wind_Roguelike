using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "draw_cardi", menuName = "Card/effect/drawi")]
public class drawCardi : CardEffectBase
{
    public int draw;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        DeckManager.Instance.DrawCards(draw);
    }

}
