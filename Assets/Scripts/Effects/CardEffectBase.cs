using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CardEffectBase : ScriptableObject
{
    public abstract void OnPlay(CardInstance self, EffectContext ctx);
    public abstract void OnDraw(CardInstance self, EffectContext ctx);
    public abstract void OnTurnEnd(CardInstance self, EffectContext ctx);
    public abstract void OnDiscard(CardInstance self, EffectContext ctx);
}