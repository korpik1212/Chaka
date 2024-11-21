using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObject : MonoBehaviour, IPointerClickHandler
{
    public Card card;
    public Image cardImage;
    public TextMeshProUGUI manaCostText;


    //TODO: Set the followign 2 variable

    public HandManager handManager;
    public int slot;

    public void SetCard(Card card)
    {
        this.card = card;
        Debug.Log(card.cardData);
        cardImage.sprite= card.cardData.cardSprite;
                
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("card clicked");
        AbilityTargetHandler.instance.SelectCard(this);
    }


    public void Select()
    {
        card.Select();
        AbilityTargetHandler.instance.SelectCard(this);

    }


    public void Cast(AbilityTarget abilityTarget)
    {
        card.Cast(abilityTarget);
        handManager.GetNewCardAtSlot(slot);
    }

    public void RemoveEffect()
    {
        //TODO get the current carddata, create a temproray effect with that card data 
    }

    public void DrawCardEffect()
    {
       
    }
}
