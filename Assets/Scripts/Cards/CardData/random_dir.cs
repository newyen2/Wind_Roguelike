using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "randdir", menuName = "Card/effect/randdir")]
public class random_dir : CardEffectBase
{
    CardInstance thiscard;
    public int dir;
    public override void OnDraw(CardInstance self, EffectContext ctx)
    {
        thiscard =self;
        dir = Random.Range(0, 4);
        CardDirection direction = self.data.direction;
        bool[] dirs = { direction.down, direction.up, direction.left, direction.right };
        for (int i = 0; i < 4; i++)
        {
            if (i == dir) dirs[i] = true;
            else dirs[i] = false;
        }
        self.data.changeDir(dirs);
        self.direction = self.data.direction;
    }
}
