using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstance
{
    // Start is called before the first frame update
    public CardData data;

    public int currentCost;
    public int currentWindPower;
    public CardDirection direction;
    public bool isExhausted;     // 用過就移出本局？
    public bool isTemporary;     // 戰鬥結束消失？
    public bool isUpgraded;      // 升級版？

    public CardInstance(CardData data)
    {
        this.data = data;
        currentCost = data.baseCost;
        currentWindPower = data.baseWindPower;
        direction = data.direction;
    }

    public virtual void cardeffect() { }
}
