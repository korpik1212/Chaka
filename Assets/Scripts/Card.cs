using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object.Synchronizing;

public class Card
{

    public Card(CardData cardData)
    {
        this.cardData = cardData;
    }
    public class CardState
    {
        public Guid abilityID;

        //public int manaCost;
        public int attackDamage;
        public SyncVar<int> manaCost = new SyncVar<int>();
       public Dictionary<string,float> extraEffects= new Dictionary<string, float>();


    }
    public CardData cardData;
    public CardState cardState;
    public virtual void Discard()
    {

    }
    //TODO: might be redundant
    public virtual void Select()
    {

    }


    public void Cast(AbilityTarget abilityTarget)
    {

    }
}



public class AbilityTarget
{
    public EnemyObject enemy;
}