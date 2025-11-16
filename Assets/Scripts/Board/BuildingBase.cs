using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    public int id;
    public string displayName;
    [TextArea] public string description;
    public int multiplier; //乘法器
    public int adder; //加法器

    public int getMultiplier()
    {
        return multiplier;
    }
    public int getAdder()
    {
        return adder;
    }
    public virtual int Score(int windPower, Direction windDirection, int x, int y)
    {
        // 基本建築預設的計分方式
        return windPower * multiplier + adder;
    }

}
