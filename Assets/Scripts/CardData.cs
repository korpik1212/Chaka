using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    public float defaultManaCost;
    public AbilityTargetHandler.TargetingType targetingType;
}
