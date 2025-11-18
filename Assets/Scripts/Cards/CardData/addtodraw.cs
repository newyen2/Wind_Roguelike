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
    bool flag;
    public override void OnDraw(CardInstance self, EffectContext ctx)
    {
        if (flag) self.currentWindPower += power_addi;
        flag = false;
        self.data.description = "【隨機方向】\n\n造成" + (self.currentWindPower) + "點風力\n" + "每打出一次風力+"+ (power_addi);
    }

    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        flag = true;
    }
    public override void OnDiscard(CardInstance self, EffectContext ctx)
    {
        //if(flag)self.currentWindPower += power_addi;
        //flag = false;
    }
    public override void OnReset(CardInstance self, EffectContext ctx)
    {
        flag = false;
        self.currentWindPower = self.data.baseWindPower;
        self.data.description = "造成" + (self.currentWindPower) + " 風力\n 隨機方向\n" + "每打出一次風力+" + (power_addi);
    }

}
