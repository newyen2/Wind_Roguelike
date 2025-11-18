using System.Collections.Generic;

public class Typhoon : BuildingBase
{
    List<int> windRecord = new();
    int wind_dir = 2;
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
            
            if(windRecord[0] == windRecord[1])
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
        wind_dir = 2;
        windRecord = new();
    }
}

