using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceNoodle : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
            StageManager.Instance.delayDraw = 0;
        }
        if (windPower <= 2)
        {
            StageManager.Instance.delayDraw += 1;
            total_point += 10;
            return 10;

        }

        return 0;
    }
}
