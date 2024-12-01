using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rename global card manager 
public class DeckManager : MonoBehaviour
{
    //bir kart destene eklendiði anda burdaki listeye kle 
    public class DeckState
    {
        public SyncList<CardState> cards= new SyncList<CardState>();

    }
    public HandManager handManager;
    public List<Card> cards = new List<Card>();

    public DeckState deckState = new DeckState();



    //TODO: Remove This
    public CardData debugData;
    private void Start()
    {
        AddDebugCards();
    }

    public void AddDebugCards()
    {
        cards.Add(new Card(debugData));
        cards.Add(new Card(debugData));
        cards.Add(new Card(debugData));
        cards.Add(new Card(debugData));
        cards.Add(new Card(debugData));
    }


    //observerrpc
    public void SyncState(DeckState state)
    {
        /*
        deckState = state;

        cards.Clear();
        foreach (CardState cardState in deckState.cards)
        {
            Card card = new Card(cardState);
            card.cardState=cardState;
            cards.Add(card);
        }
       */

    }

    //SERVERRPC
    public void RegisterCardServerRPC(CardState cardState)
    {
        //auth check
        cardState.abilityID=Guid.NewGuid();
        AddCardObserverRPC(cardState);
    }

    //OBSERVERRPC

    public void AddCardObserverRPC(CardState cardState)
    {

        deckState.cards.Add(cardState);
        
    }
    //TODO: Return a random card that isnt in the hand instead
    public Card GetNextCard()
    {

        return cards[0];
    }


    public void ModifyExtraEffect(Guid abilityID,string effectName, float value)
    {
        
    }

    public void ModifyDamage(Guid abilityID,float damage)
    {

    }


    public DeckState GetDeckState()
    {
        DeckState deckState = new DeckState();
        foreach(Card c in cards)
        {
            deckState.cards.Add(c.cardState);
        }
        return deckState;
    }

}
