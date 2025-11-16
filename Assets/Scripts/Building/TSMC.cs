using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

public class TSMC : BuildingBase
{
    int now_pts = 0;
    public override int EndScore(int x, int y)
    {
        if (StageManager.Instance.round != now_round)
        {
            StageManager.Instance.delayDraw = 0;
            Renew(StageManager.Instance.round);
        }

        var grid = GlobalManager.Instance.grid;
        if (grid == null)
        {
            now_pts = 0;
            return now_pts;
        }

        int pts = 0;
        int w = grid.GetLength(0);
        int h = grid.GetLength(1);

        void AddIfValid(int ix, int iy)
        {
            if (ix >= 0 && ix < w && iy >= 0 && iy < h)
            {
                var go = grid[ix, iy];
                if (go != null)
                {
                    var bb = go.GetComponent<BuildingBase>();
                    pts += bb.total_point;
                }
            }
        }

        AddIfValid(x - 1, y); // left
        AddIfValid(x + 1, y); // right
        AddIfValid(x, y - 1); // down
        AddIfValid(x, y + 1); // up

        if(pts >= 80)
        {
            StageManager.Instance.delaycost += 1;
            total_point += 30;
            return 30;
        }
        return 0;
    }
}

