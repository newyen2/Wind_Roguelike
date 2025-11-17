using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "addpower", menuName = "Card/effect/addpower")]
public class addpower : CardEffectBase
{
    CardInstance thiscard;
    public int power_addi;
    public override void OnDraw(CardInstance self, EffectContext ctx)
    {
        self.data.description = "造成" + (self.currentWindPower + 5) + " 風力\n" + "每打出一次風力+5\n初始為6";
    }
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        self.currentWindPower += power_addi;
    }
}
