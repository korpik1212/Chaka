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

    public CardObject cardObjectPrefab;


    public InGamePlayerManager playerManager;


    public PlayerGameCharacter playerGameCharacter;
    // when you play a card you get a new card on the exact same spot
    //there might be discard effects that get rid of a card without playing 
    //there might be targeted draw effects 

    // TODO: temporary, remove this 
    private void Start()
    {
        playerManager = GetComponentInParent<InGamePlayerManager>();
        playerGameCharacter = GetComponentInParent<PlayerGameCharacter>();
        GetDebugCards();
    }

    public CardData debugData;
    public CardData debugData2;


    //TODO: occupying card baþlangýçta boþ olmalý 
    public void GetDebugCards()
    {

     foreach(HandSlot slot in handSlots)
        {
            Card card = new Card(debugData);
            CardObject cardObject = Instantiate(cardObjectPrefab, slot.transform);
            cardObject.InitializeCardObject(card, slot,playerManager);
            cardObject.OnCardCast.AddListener(GetNewCardAtSlot);
        }
        handSlots[2].currentlyOccupyingCardObject.DestroyCard();
        Card card2 = new StrikeCard(debugData2);
        CardObject cardObject2 = Instantiate(cardObjectPrefab, handSlots[2].transform);
        cardObject2.InitializeCardObject(card2, handSlots[2],playerManager);
        cardObject2.OnCardCast.AddListener(GetNewCardAtSlot);


    }
    public void GetNewCardAtSlot(int slot)
    {

        Card card = deckManager.GetNextCard();
        CardObject cardObject = Instantiate(cardObjectPrefab, handSlots[slot].transform);
        cardObject.InitializeCardObject(card, handSlots[slot], playerManager);
        cardObject.OnCardCast.AddListener(GetNewCardAtSlot);

        //  AddNextCardServerRPC(slot);
    }

    private void Update()
    {
        if (!playerGameCharacter.IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCard(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCard(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCard(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCard(3);
    }

    public void SelectCard(int slot)
    {
        handSlots[slot].currentlyOccupyingCardObject.SelectCard();

        selectedCard = handSlots[slot].currentlyOccupyingCardObject.card;
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
        /*
        RemoveCard(slot);
        AddCard(cardState, slot);
        */
    }

   
}
