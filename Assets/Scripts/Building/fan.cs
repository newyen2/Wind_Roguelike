using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan  : BuildingBase
{
    public int addddd;
    public override int StartScore(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        if (StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        wind.power += addddd;


        return 0;
    }
}
