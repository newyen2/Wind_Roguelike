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
        
        if(windDirection == Direction.W)
        {
            AudioManager.Instance.Play("kongming");
            wind.power *= 2;
            return -100001;
        }

        return -100000; // 阻擋改成 -100000;
    }

    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        return 0;
    }
}

