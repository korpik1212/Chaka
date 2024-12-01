using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibary : MonoBehaviour
{
    public static CardLibary instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        if (instance == null) instance = this;
    }
    public List<Card> cards = new List<Card>();
    public Dictionary<string, CardData> cardDatas = new Dictionary<string, CardData>();
    private void Start()
    {

        ConstructDataList();
        ConstructCardsList();
    }

    public void ConstructDataList()
    {
        var allAvaibleCardDatas = Resources.LoadAll<CardData>("Cards");

        foreach(CardData c in allAvaibleCardDatas)
        {
            cardDatas.Add(c.cardName, c);
        }
    }
  
         //TODO: Populate this 
    public void ConstructCardsList()
    {
        cards.Add(new DenemeCard(cardDatas["DenemeCard"]));
    }



   public CardData GetCardData(string cardName)
    {
        return cardDatas[cardName];
    }
}
