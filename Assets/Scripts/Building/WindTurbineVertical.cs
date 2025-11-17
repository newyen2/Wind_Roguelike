using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineVertical : BuildingBase
{
    public override int StartScore(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(windDirection == Direction.E)
        {
            return -100000;
        }
        if(windDirection == Direction.W){

            return -100000;
        }
        return 0;
    }
    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }

        if(windDirection == Direction.N)
        {
            total_point = windPower * multiplier;
            return windPower * multiplier;
        }
        if(windDirection == Direction.S){
            total_point = windPower * multiplier;
            return windPower * multiplier;
        }

        return 0;
    }
}
