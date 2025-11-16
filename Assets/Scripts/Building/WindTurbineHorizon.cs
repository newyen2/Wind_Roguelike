using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbineHorizon : BuildingBase
{
    public override int Score(int windPower, Direction windDirection, int x, int y)
    {
        if(windDirection == Direction.E) return windPower * multiplier;
        if(windDirection == Direction.W) return windPower * multiplier;

        return 0;
    }
}

