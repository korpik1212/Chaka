using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intent", menuName = "Intent")]



//Cleanup: bunu yapman�n daha iyi bir yolu olmas� gerek, farkl� intent tipleri direkt kendini managelasa damage verse felan yada buda mant�ks�z de�il gibi 
public class Intent_SO : ScriptableObject
{ 

    public enum IntentType
    {
        Attack,
        Skip
    }
    public IntentType intentType;
    public Sprite intentSprite;
    public Color intentChargeColor;
    public float IntentCD;
    public float intentValue;
}
