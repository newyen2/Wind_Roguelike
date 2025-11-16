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

        var building = GlobalManager.Instance.grid[x, y]?.GetComponent<BuildingBase>();
        if (building != null)
        {
            int score = building.Score(power, direction, x, y,this);
            StageManager.Instance.score += score;
            UIManager.Instance.DisplayScoreText(StageManager.Instance.score);
            UIManager.Instance.DisplayTextScoreParticle(x, y, score);
            Debug.Log($"Wind Execute: {x}, {y} = {score}");
            if(score>0)
            {
                AudioManager.Instance.Play("get_point");
            }
        }
        else 
        { 
            Debug.Log($"Wind Execute: {x}, {y} = 0"); 
        }

    }

    public void Move(int x, int y)
    {
        if (!isEnable) {
            return;
        }
        Debug.Log(StageManager.Instance.nextWindPosition);
        UIManager.Instance.DisplayWindParticle(x, y, direction);
        if (direction == Direction.E)
        {   
            if(x >= GlobalManager.Instance.groundSize + 1)
            {
                isEnable = false;
                return;
            }
            int score = (x + 1 <= GlobalManager.Instance.groundSize) ? GlobalManager.Instance.grid[x+1, y]?.GetComponent<BuildingBase>().StartScore(power ,direction, x+1, y, this) ?? 0 : 0;
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x+1}, {y})");
            if(score != -999){
                StageManager.Instance.nextWindPosition[x + 1, y].windSlot.Add(new Wind(direction, power));
                StageManager.Instance.score += score;
            }
            else UIManager.Instance.DisplayTextScoreParticle(x+1, y, -1);
        }
        else if (direction == Direction.W)
        {
            if (x <= 1)
            {
                isEnable = false;
                return;
            }
            int score = (x - 1 >= 1) ? GlobalManager.Instance.grid[x-1, y]?.GetComponent<BuildingBase>().StartScore(power ,direction,x-1, y, this) ?? 0 : 0;
            
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x-1}, {y})");
            if(score != -999){
                StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(new Wind(direction, power));
                StageManager.Instance.score += score;
            }
            else UIManager.Instance.DisplayTextScoreParticle(x-1, y, -1);
        }
        else if (direction == Direction.S)
        {
            if (y <= 1)
            {
                isEnable = false;
                return;
            }
            int score = (y - 1 >= 1) ? GlobalManager.Instance.grid[x, y-1]?.GetComponent<BuildingBase>().StartScore(power ,direction,x, y-1, this) ?? 0 : 0;
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y - 1})");
            if(score != -999){
                StageManager.Instance.nextWindPosition[x, y - 1].windSlot.Add(new Wind(direction, power));
                StageManager.Instance.score += score;
            }
            else UIManager.Instance.DisplayTextScoreParticle(x, y-1, -1);
        }
        else if (direction == Direction.N)
        {
            if (y >= GlobalManager.Instance.groundSize + 1)
            {
                isEnable = false;
                return;
            }
            int score = (y + 1 <= GlobalManager.Instance.groundSize) ? GlobalManager.Instance.grid[x, y+1]?.GetComponent<BuildingBase>().StartScore(power ,direction,x, y+1, this) ?? 0 : 0;
            StageManager.Instance.score += score;
            Debug.Log($"Wind Move: ({x}, {y}) -> ({x}, {y + 1})");
            if(score != -999){
                StageManager.Instance.nextWindPosition[x - 1, y].windSlot.Add(new Wind(direction, power));
                StageManager.Instance.score += score;
            }
            else UIManager.Instance.DisplayTextScoreParticle(x, y+1, -1);
        }
    }

}
