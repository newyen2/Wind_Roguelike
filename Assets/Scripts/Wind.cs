using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind
{
    public Direction direction; // Changed type from int to Direction  
    public bool isEnable;

    public Wind(Direction direction)
    {

        this.direction = direction;
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
            StageManager.Instance.nextWindPosition[x + 1, y].windSlot.Add(new Wind(direction));
        }
        else if (direction == Direction.W)
        {
            if (x <= 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x - 1}, {y})");
            StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(new Wind(direction));
        }
        else if (direction == Direction.S)
        {
            if (y <= 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y - 1})");
            StageManager.Instance.nextWindPosition[x, y - 1].windSlot.Add(new Wind(direction));
        }
        else if (direction == Direction.N)
        {
            if (y >= GlobalManager.Instance.groundSize + 1)
            {
                isEnable = false;
                return;
            }
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y + 1})");
            StageManager.Instance.nextWindPosition[x, y + 1].windSlot.Add(new Wind(direction));
        }
    }

}
