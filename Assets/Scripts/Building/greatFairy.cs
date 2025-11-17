using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greatFairy : BuildingBase
{

    public override int WhenStart(int x, int y)
    {
        DeckManager.Instance.Exhaustcost += 1;
        DeckManager.Instance.ExhaustDraw += 1;
        DeckManager.Instance.DrawCards(1);
        StageManager.Instance.powerPoint += 1;
        return 0;
    }
    public override int EndScore(int x, int y)
    {
        StageManager.Instance.delaycost += 1;
        StageManager.Instance.delayDraw += 1;
        return -50;
    }
}
