using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cirno : BuildingBase
{


    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
       this.multiplier = 1;

        if (StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        if (windPower == 9)
        {
            this.total_point = 99;
            AudioManager.Instance.Play("baka");
        }

        return this.total_point* multiplier;
    }
}

