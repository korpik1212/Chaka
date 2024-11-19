using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rename global card manager 
public class CardManager : MonoBehaviour
{
    //bir kart destene eklendiði anda burdaki listeye kle 
    public List<Card.CardState> cardStates = new List<Card.CardState>();
    

    //SERVERRPC
    public void RegisterCardServerRPC(Card.CardState cardState)
    {
        //auth check
        cardState.abilityID=Guid.NewGuid();
        AddCardObserverRPC(cardState);
    }

    //OBSERVERRPC

    public void AddCardObserverRPC(Card.CardState cardState)
    {

        cardStates.Add(cardState);
        
    }
}
