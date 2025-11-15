using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public string id;
    public string displayName;
    [TextArea] public string description;
    public int multiplier; //乘法器
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
