using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "add_energyi", menuName = "Card/effect/addenergyi")]
public class addenergyi : CardEffectBase
{
    public int cost;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        DeckManager.Instance.DrawCards(cost);
    }

}
