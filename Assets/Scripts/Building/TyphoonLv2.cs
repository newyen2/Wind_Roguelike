using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonLv2 : BuildingBase
{
    List<int> windRecord = new();
    int wind_dir = 3;
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if(StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }

        if(windDirection == Direction.N) wind_dir -= 1;
        if(windDirection == Direction.S) wind_dir -= 1;
        if(windDirection == Direction.E) wind_dir -= 1;
        if(windDirection == Direction.W) wind_dir -= 1;
        windRecord.Add(windPower);

        if(wind_dir == 0){
            
            if(windRecord[0] == windRecord[1] && windRecord[1] == windRecord[2] && windRecord[2] == windRecord[3])
            {
                total_point += windPower * multiplier; 
                return windPower * multiplier;
            }
        }
        return 0;
    }

    public override void Renew(int round)
    {
        now_round = round;
        total_point = 0;
        wind_dir = 3;
        windRecord = new();
    }
}

