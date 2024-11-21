using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    //TODO: Make this a network behaviour 

    public class HandState
    {
        public List<CardState> cards = new List<CardState>();
    }

    public HandState handState = new HandState();

    public List<HandSlot> handSlots = new List<HandSlot>();

    public Card selectedCard = null;
    public DeckManager deckManager;

    // when you play a card you get a new card on the exact same spot
    //there might be discard effects that get rid of a card without playing 
    //there might be targeted draw effects 

    // TODO: temporary, remove this 
    private void Start()
    {
        GetDebugCards();
    }

    public CardData debugData;
    public CardData debugData2;
    public void GetDebugCards()
    {

     foreach(HandSlot slot in handSlots)
        {
            slot.occupyingCard.card = new Card(debugData);
        }

        handSlots[2].occupyingCard.card = new Card(debugData2);
    }
    public void GetNewCardAtSlot(int slot)
    {
        AddNextCardServerRPC(slot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCard(3);
    }

    public void SelectCard(int slot)
    {
        Debug.Log("try select");
        handSlots[slot].occupyingCard.Select();

        selectedCard = handSlots[slot].occupyingCard.card;
    }


    void RemoveCard(int slot)
    {
        handSlots[slot].occupyingCard.RemoveEffect();
        handSlots[slot].occupyingCard.card = null;
    }

    void AddCard(CardState card,int slot)
    {
        handSlots[slot].occupyingCard.card = new Card(card);
        handSlots[slot].occupyingCard.card.cardData = debugData;
        handSlots[slot].occupyingCard.SetCard(handSlots[slot].occupyingCard.card);
    }

    //Server RPc
    void AddNextCardServerRPC(int slot)
    {
        //networking stuff 
       Card card= deckManager.GetNextCard();
       AddNextCardObserverRPC(card.cardState,slot);


    }

    //Observer rpc
    void AddNextCardObserverRPC(CardState cardState,int slot)
    {
        RemoveCard(slot);
        AddCard(cardState, slot);
    }

   
}
