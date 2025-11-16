using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y)
    {
        if(StageManager.Instance.round != now_round)
        {
            total_point = windPower * multiplier;
            Renew(StageManager.Instance.round);
        }

        return windPower * multiplier;
    }
}

