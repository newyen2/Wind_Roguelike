using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyphoonLv2 : BuildingBase
{
    List<int> windRecord = new();
    int wind_dir = 4;

    
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {

        wind_dir -= 1;
        windRecord.Add(windPower);

        if(wind_dir == 0){
            
            if(windRecord[0] == windRecord[1] && windRecord[1] == windRecord[2] && windRecord[2] == windRecord[3])
            {
                total_point += windPower * multiplier; 
                return windPower * multiplier;
            }
        }
        if (wind_dir < 0)
        {
            if(windPower == windRecord[0])
            {
                total_point += windPower * multiplier;
                return windPower * multiplier / 2;
            }
        }
        return 0;
    }

    public override void Renew(int round)
    {
        now_round = round;
        total_point = 0;
        wind_dir = 4;
        windRecord = new();
        base.Renew(round);
    }
}

