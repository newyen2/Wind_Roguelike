using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPos : BuildingBase
{
    public CardData cirnoCard;
    CardInstance cirnoCardIns;
    public override int Score(int windPower, Direction windDirection, int x, int y,Wind wind)
    {
        cirnoCardIns = new CardInstance(cirnoCard);
        cirnoCardIns.isExhausted = true;
        cirnoCardIns.isTemporary = true;

        this.multiplier = 1;

        if (StageManager.Instance.round != now_round)
        {
            Renew(StageManager.Instance.round);
        }
        for(int i = 0; i < adder; i++) DeckManager.Instance.AddCardToDiscardPile(cirnoCardIns);
        return windPower * multiplier;
    }
}

