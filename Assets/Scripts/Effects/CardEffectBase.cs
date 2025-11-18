using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectBase : ScriptableObject
{

    public virtual void OnPlay(CardInstance self, EffectContext ctx) {

    }//見BuildWindManager.cs的trybuild
    public virtual void OnDraw(CardInstance self, EffectContext ctx) {
    
    }//見DeckManager.cs的DrawCards
    public virtual void OnTurnEnd(CardInstance self, EffectContext ctx) { 
    
    }//見StageManager.cs的NextRound
    public virtual void OnDiscard(CardInstance self, EffectContext ctx) { 
    
    }//見DeckManager.cs的DiscardFromHand

    public virtual void OnReset(CardInstance self, EffectContext ctx) { }

}