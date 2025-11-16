using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayGap : BuildingBase
{
    // 效果 : 使經過的風強制朝下
    public override int StartScore(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        if (StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        if(windDirection != Direction.S)
        {
            AudioManager.Instance.Play("let_me_see_see");
            wind.direction = Direction.S;
        }
        return 0;
    }

    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        return windPower * multiplier;
    }
}

