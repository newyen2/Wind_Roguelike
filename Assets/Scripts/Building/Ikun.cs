using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ikun : BuildingBase
{
    public CardData cirnoCard;
    CardInstance cirnoCardIns;
    public override int StartScore(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        int newWindPower = windPower * 3 / 2;
        if (windDirection == Direction.E)
        {
            if(y < 4) StageManager.Instance.nextWindPosition[x + 1, y + 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
            if(y > 1) StageManager.Instance.nextWindPosition[x + 1, y - 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
        }
        else if (windDirection == Direction.N)
        {
            if(x > 1) StageManager.Instance.nextWindPosition[x - 1, y + 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
            if(x < 4) StageManager.Instance.nextWindPosition[x + 1, y + 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
        }
        else if (windDirection == Direction.S)
        {
            if(x > 1) StageManager.Instance.nextWindPosition[x - 1, y - 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
            if(x < 4) StageManager.Instance.nextWindPosition[x + 1, y - 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
        }
        else if (windDirection == Direction.W)
        {
            if(y < 4) StageManager.Instance.nextWindPosition[x - 1, y + 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
            if(y > 1) StageManager.Instance.nextWindPosition[x - 1, y - 1].windSlot.Add(new Wind(windDirection, Math.Min(1,newWindPower)));
        }
        return -100002;
    }
}

