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
    public int total_point = 0;
    public int now_round = -1;

    [Header("UI 元件")]
    public int min_round = 0, max_round = 9, weight = 1;

    public int getMultiplier()
    {
        return multiplier;
    }
    public int getAdder()
    {
        return adder;
    }
    public virtual int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        // 基本建築預設的計分方式
        return 0;
    }

    public virtual int WhenStart(int x, int y)
    {
        return 0;
    }

    public virtual int EndScore(int x, int y)
    {
        // 基本建築預設的計分方式
        return 0;
    }

    public virtual int StartScore(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        // 基本建築預設的計分方式
        return 0;
    }

    public virtual int roundStart(int round) { Renew(round); return 0; }
    public virtual void Renew(int round)
    {
        now_round = round;
        total_point = 0;
    }
}
