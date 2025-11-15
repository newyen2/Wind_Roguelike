using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlot
{
    public List<Wind> windSlot = new List<Wind>();
    public int x;
    public int y;
    public int slotSize = 100;
    public int inputDirection; // -1: none, 0: E, 1: W, 2: S, 3: N

    public void Clear()
    {
        windSlot.Clear();
    }

    public void Execute()
    {
        foreach(Wind wind in windSlot)
        {
            wind.Execute();
        }

        for (int i = 0; i < GlobalManager.Instance.groundSize; i++)
        {
            foreach (Wind wind in windSlot)
            {
                if (wind.direction == Direction.E)
                {
                    StageManager.Instance.nextWindPosition[x + 1, y].windSlot.Add(wind);
                }
                else if (wind.direction == Direction.W)
                {
                    StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(wind);
                }
                else if (wind.direction == Direction.S)
                {
                    StageManager.Instance.nextWindPosition[x, y - 1].windSlot.Add(wind);
                }
                else if (wind.direction == Direction.N)
                {
                    StageManager.Instance.nextWindPosition[x, y + 1].windSlot.Add(wind);
                }
            }
        }

    }


}