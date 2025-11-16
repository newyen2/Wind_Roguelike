using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineHorizon : BuildingBase
{
    public override int StartScore(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(windDirection == Direction.S)
        {
            return -1;
        }
        if(windDirection == Direction.N){

            return -1;
        }
        return 0;
    }
    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        
        if(windDirection == Direction.E)
        {
            total_point = windPower * multiplier;
            return windPower * multiplier;
        }
        if(windDirection == Direction.W){
            total_point = windPower * multiplier;
            return windPower * multiplier;
        }

        return 0;
    }
}

