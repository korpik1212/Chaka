using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Intent", menuName = "Intent")]



//Cleanup: bunu yapmanýn daha iyi bir yolu olmasý gerek, farklý intent tipleri direkt kendini managelasa damage verse felan yada buda mantýksýz deðil gibi 
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
