using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CardEffectBase : ScriptableObject
{
    // 戰鬥型
    //public virtual void OnPlay(BattleContext ctx, CardInstance card) {}
    //public virtual void OnAfterResolved(BattleContext ctx, CardInstance card) {}

    // 如果是小丑牌系統，也可以加：
    //public virtual void OnPlay(RunContext run, HandContext hand, CardInstance card) {}
}