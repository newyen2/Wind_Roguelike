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

        for (int i = 0; i < GlobalManager.Instance.groundSize; i++)
        {
            foreach (Wind wind in windSlot)
            {
                wind.Execute();
            }

            foreach (Wind wind in windSlot)
            {
                wind.Move(x, y);
            }
        }

    }


}