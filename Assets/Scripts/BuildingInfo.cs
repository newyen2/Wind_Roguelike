using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    public int id;
    public string displayName;
    [TextArea] public string description;
    public int multiplier; //­¼ªk¾¹
    public int adder; //

    public int getMultiplier()
    {
        return multiplier;
    }
    public int getAdder()
    {
        return adder;
    }
}

