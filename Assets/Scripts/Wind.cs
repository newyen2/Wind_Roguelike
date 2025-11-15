using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wind : MonoBehaviour
{
    public Direction direction; // Changed type from int to Direction  

    // Start is called before the first frame update  
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {

    }

    public void Create(Direction inputDirection)
    {
        direction = inputDirection;
    }

    public void Execute()
    {

    }

    public void Move(int x, int y)
    {
        if (direction == Direction.E)
        {
            StageManager.Instance.nextWindPosition[x + 1, y].windSlot.Add(this);
        }
        else if (direction == Direction.W)
        {
            StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(this);
        }
        else if (direction == Direction.S)
        {
            StageManager.Instance.nextWindPosition[x, y - 1].windSlot.Add(this);
        }
        else if (direction == Direction.N)
        {
            StageManager.Instance.nextWindPosition[x, y + 1].windSlot.Add(this);
        }
    }

}
