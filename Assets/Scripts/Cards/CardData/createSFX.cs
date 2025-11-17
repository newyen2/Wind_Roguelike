using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "createSFX", menuName = "Card/effect/createSFX")]
public class createSFX :CardEffectBase
{
    public string SFXname;
    public override void OnPlay(CardInstance self, EffectContext ctx)
    {
        AudioManager.Instance.Play(SFXname);
    }
}
