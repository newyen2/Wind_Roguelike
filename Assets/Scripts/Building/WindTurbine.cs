using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
            total_point += windPower * multiplier;

        return windPower * multiplier;
    }
}

