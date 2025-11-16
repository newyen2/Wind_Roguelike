using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CardEffectBase : ScriptableObject
{
    public abstract void OnPlay(CardInstance self, EffectContext ctx);//見BuildWindManager.cs的trybuild
    public abstract void OnDraw(CardInstance self, EffectContext ctx);//見DeckManager.cs的DrawCards
    public abstract void OnTurnEnd(CardInstance self, EffectContext ctx);//見StageManager.cs的NextRound
    public abstract void OnDiscard(CardInstance self, EffectContext ctx);//見DeckManager.cs的DiscardFromHand
}