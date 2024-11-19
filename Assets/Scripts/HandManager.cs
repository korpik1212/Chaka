using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    //TODO: Make this a network behaviour 

    public class HandState
    {
        public List<Card.CardState> cards = new List<Card.CardState>();
    }

    public HandState handState = new HandState();

    public List<HandSlot> handSlots = new List<HandSlot>();

    public Card selectedCard = null;

    // when you play a card you get a new card on the exact same spot
    //there might be discard effects that get rid of a card without playing 
    //there might be targeted draw effects 
    public void GetNewCardAtSlot(int slot)
    {
        RemoveCard(slot);
        AddNextCard(slot);
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
        handSlots[slot].occupyingCard.Select();

        selectedCard = handSlots[slot].occupyingCard.card;
    }


    void RemoveCard(int slot)
    {
        //networking stuff
    }

    void AddNextCard(int slot)
    {
        //networking stuff 
    }

   
}
