using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardObject : MonoBehaviour, IPointerClickHandler
{
    public Card card;
    public int asd;
    public void OnPointerClick(PointerEventData eventData)
    {
    }


    public void Select()
    {
        card.Select();
    }
}
