using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineMin : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }

        if(windPower <= 2) return windPower * multiplier;
        return 0;
    }
}

