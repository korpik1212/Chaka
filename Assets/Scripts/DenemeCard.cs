using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeCard : Card
{
    CardManager abilityManager;

    public int counter;
  
   
    public int token;

    public DenemeCard(CardData cardData) : base(cardData)
    {

    }

    public void SyncState(CardState state)
    {
        cardState = state;
    }


    public void UpdateState()
    {

      
    }

    public void GainCounter()
    {

        counter++;
        Debug.Log("GainToken");
       // abilityManager.SyncCardState(cardState);

    }
}
