using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour
{
    [HideInInspector]
    public CardObject currentlyOccupyingCardObject;
    public Image selectedBorder;

    public int slotIndex;
    public void OnCardSelected()
    {
     
        selectedBorder.gameObject.SetActive(true);

    }

    public void OnCardDeSelected()
    {
        selectedBorder.gameObject.SetActive(false);

    }

}
