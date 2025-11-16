using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineMax : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }

        if(windPower >= 10)
        {
            total_point = windPower * multiplier;
            return windPower * multiplier;
        }
        return 0;
    }
}

