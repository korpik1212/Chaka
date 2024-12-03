using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object.Synchronizing;

public class Card
{


    //TODO I'm not sure if I should construct it with card data or card state or nothing at all 
 
    //get rid of state class and put them inside this class as sync vars 
    //later make a list of Card class in hand manager and deck manager 

    public Card(CardData cardData)
    {
        this.cardData = cardData;
        //TODO: set card state

        cardState = new CardState();
      //  cardState.manaCost = (int)cardData.defaultManaCost;
    }

  
  
    public CardData cardData;
    public CardState cardState;
    public virtual void Discard()
    {

    }
    // Cleanup: might be redundant, looks reduntant
    public virtual void Select()
    {

    }


    public virtual void Cast(AbilityTarget abilityTarget)
    {



        if(cardData.targetingType == AbilityTargetHandler.TargetingType.Enemy)
        {
            Debug.Log("ability getting casted at" + abilityTarget.enemy.name);
        }
        else
        {
            Debug.Log("target free");
        }
    }



}

public class AbilityTarget
{

    public EnemyObject enemy;
}


public class CardState
{
    public Guid AbilityDataID;
    public Guid AbilityReffrenceID;


   //public int manaCost;
   public Dictionary<string, float> values = new Dictionary<string, float>();


}