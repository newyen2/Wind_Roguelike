using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aya :BuildingBase
{
    int finalMut=-1;

    public override void Renew(int round)
    {
        finalMut = -1;
        base.Renew(round);
    }
    public override int roundStart(int round)
    {
        if (round == 1)
        {
            Renew(round);
        }
        StageManager.Instance.finalMut += finalMut;
        print(finalMut);
        print(StageManager.Instance.finalMut);
        Renew(round);
        return 0;
    }
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        finalMut += 1;
        return 0;
    }
}
