using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cirno : BuildingBase
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
        if (windPower == 9)
        {
            this.total_point += 99;
            AudioManager.Instance.Play("baka");
            DeckManager.Instance.AddCardToDiscardPile(cirnoCardIns);
            return 99;
        }
        this.total_point += 9;
        return 9;
    }
}

