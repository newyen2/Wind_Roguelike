using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : BuildingBase
{

    public override int Score(int windPower, Direction windDirection, int x, int y)
    {
        // 基本建築預設的計分方式
        return windPower * multiplier;
    }

}

