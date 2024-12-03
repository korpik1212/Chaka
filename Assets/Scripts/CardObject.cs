using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CardObject : MonoBehaviour, IPointerClickHandler
{
    public Card card;
    public Image cardImage;
    public TextMeshProUGUI manaCostText;

    [HideInInspector]
    public HandSlot handSlot;
    //TODO: Set the followign 2 variable



    public UnityEvent<int> OnCardCast;

    public InGamePlayerManager inGamePlayerManager;
    public void InitializeCardObject(Card card,HandSlot handSlot,InGamePlayerManager inGamePlayerManager)
    {
        this.inGamePlayerManager = inGamePlayerManager;
        this.card = card;
        this.card.cardData = card.cardData;
        cardImage.sprite= card.cardData.cardSprite;
        manaCostText.text = card.cardData.defaultManaCost.ToString();
        this.handSlot = handSlot;
        handSlot.currentlyOccupyingCardObject = this;
        
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        SelectCard();
    }

    public void SelectCard()
    {
        card.Select();
        AbilityTargetHandler.instance.SelectCard(this);
        Debug.Log(handSlot);
        handSlot.OnCardSelected();
    }



    public void Cast(AbilityTarget abilityTarget)
    {
        float manaCost= card.cardData.defaultManaCost;

        if(manaCost> inGamePlayerManager.manaManager.currentMana)
        {
            Debug.Log("Not enough mana");
            return;
        }

        inGamePlayerManager.manaManager.SpendMana(manaCost); 
        //managing mana manager reffrence here is not very smart, hmm maybe player reffrence ? 

        handSlot.OnCardDeSelected();
        card.Cast(abilityTarget);
        OnCardCast?.Invoke(handSlot.slotIndex);
        DestroyCard();
    }

    public void DestroyCard()
    {
        this.transform.DOMoveY(500, 0.5f).SetEase(Ease.InOutBack);
        Destroy(this.gameObject,0.5f);
        //destro
        //TODO get the current carddata, create a temproray effect with that card data 
    }

    public void DrawCardEffect()
    {
       
    }
}
