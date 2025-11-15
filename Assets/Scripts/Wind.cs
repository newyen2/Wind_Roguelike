using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind
{
    public Direction direction; // Changed type from int to Direction  
    public int power;
    public bool isEnable;

    public Wind(Direction direction, int power)
    {

        this.direction = direction;
        this.power = power;
        this.isEnable = true;
    }


    // Start is called before the first frame update  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {

    }

    public void Execute(int x, int y)
    {
        if (!isEnable)
        {
            return;
        }
        
        if (GlobalManager.Instance.grid[x, y] != null)
        {
            StageManager.Instance.score += power * GlobalManager.Instance.grid[x, y].GetComponent<BuildingBase>().multiplier * GlobalManager.Instance.grid[x, y].GetComponent<BuildingBase>().adder;
        }
        Debug.Log($"Wind Execute: {x}, {y}");
    }

    public void Move(int x, int y)
    {
        if (!isEnable) {
            return;
        }
        Debug.Log(StageManager.Instance.nextWindPosition);
        if (direction == Direction.E)
        {   
            if(x >= GlobalManager.Instance.groundSize + 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x+1}, {y})");
            StageManager.Instance.nextWindPosition[x + 1, y].windSlot.Add(new Wind(direction, power));
        }
        else if (direction == Direction.W)
        {
            if (x <= 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x - 1}, {y})");
            StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(new Wind(direction, power));
        }
        else if (direction == Direction.S)
        {
            if (y <= 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y - 1})");
            StageManager.Instance.nextWindPosition[x, y - 1].windSlot.Add(new Wind(direction, power));
        }
        else if (direction == Direction.N)
        {
            if (y >= GlobalManager.Instance.groundSize + 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y + 1})");
            StageManager.Instance.nextWindPosition[x, y + 1].windSlot.Add(new Wind(direction, power));
        }
    }

}
