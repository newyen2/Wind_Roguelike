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

    // Constructor
    public WindSlot(int x, int y, int slotSize = 100, int inputDirection = -1)
    {
        this.x = x;
        this.y = y;
        this.slotSize = slotSize;
        this.inputDirection = inputDirection;
    }

    public void Clear()
    {
        windSlot.Clear();
    }

    public void Execute()
    {
        foreach (Wind wind in windSlot)
        {
            wind.Execute(x, y);
        }

        foreach (Wind wind in windSlot)
        {
            wind.Move(x, y);
        }
    }
}
