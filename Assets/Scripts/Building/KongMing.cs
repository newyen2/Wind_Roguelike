using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongMing : BuildingBase
{
    public override int StartScore(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        
        if(windDirection == Direction.E)
        {
            wind.power *= 2;
            return 0;
        }

        return -1;
    }
}

