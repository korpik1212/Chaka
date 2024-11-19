using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibary : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Dictionary<string, CardData> cardDatas = new Dictionary<string, CardData>();
    private void Start()
    {

        ConstructDataList();
        ConstructCardsList();
    }

    public void ConstructDataList()
    {
        var allAvaibleStatusEffectDatas = Resources.LoadAll<CardData>("Cards");

        foreach(CardData c in allAvaibleStatusEffectDatas)
        {
            cardDatas.Add(c.cardName, c);
        }
    }

    public void ConstructCardsList()
    {
        cards.Add(new DenemeCard(cardDatas["DenemeCard"]));
    }
}
