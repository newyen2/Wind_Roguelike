using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public List<Direction> directions;

    public virtual void Process()
    {

    }

}
