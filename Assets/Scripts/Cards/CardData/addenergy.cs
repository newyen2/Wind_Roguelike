using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "addenergy", menuName = "Card/effect/energy")]
public class addenergy : CardEffectBase
{
    public int addiEnergy;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        StageManager.Instance.delaycost += addiEnergy;
    }
}
