using UnityEngine;

public class FertileWind : CardEffectBase
{
    public virtual void OnPlay(CardInstance self, EffectContext ctx)
    {
        //ctx.buildWindManager.AddWindPower(2); // 假設增加2點風力
    }
}