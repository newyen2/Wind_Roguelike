using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aya : BuildingBase
{
    //要能改其它class的東西
    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        return windPower * multiplier;
    }
}

