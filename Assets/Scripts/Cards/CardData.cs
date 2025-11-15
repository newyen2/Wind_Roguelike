using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CardData", menuName = "Card/Data")]
public class CardData : ScriptableObject
{
    public string id;
    public string displayName;
    [TextArea] public string description;

    public Sprite Image;
    public int baseCost;
    public int baseWindPower;
    // 這張卡對應的效果（重點在這裡）之後再加點新東西
    //public CardEffectBase[] effects;
}
