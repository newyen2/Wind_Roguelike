using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sanae_build :BuildingBase
{
    public List<int> prob;
    public List<CardInstance> cards;
    public List<CardData> cData;
    private void Start()
    {
        if (cards==null)
        {
            cards = new List<CardInstance>();
            for (int i = 0; i < cData.Count; i++)
            {
                cards.Add(new CardInstance(cData[i]));
            }
        }
    }
    public override int Score(int windPower, Direction windDirection, int x, int y, Wind wind)
    {
        if (windPower <= 2)
        {
            int id = WeightedRandomIndex(prob);
            DeckManager.Instance.AddCardToDrawPile(cards[id],true);
        }
        total_point += multiplier * windPower;
        return multiplier * windPower;
    }

    int WeightedRandomIndex(List<int> weights)
    {
        int total = 0;

        // 計算加權總和
        for (int i = 0; i < weights.Count; i++)
            total += weights[i];

        // 隨機一個 total 範圍內的值
        int r = Random.Range(0, total);

        // 決定落在哪個區間
        for (int i = 0; i < weights.Count; i++)
        {
            if (r < weights[i])
                return i;

            r -= weights[i];
        }

        // 理論上不會走到這裡
        return weights.Count - 1;
    }


}
