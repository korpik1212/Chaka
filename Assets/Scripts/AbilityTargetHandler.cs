using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetHandler : MonoBehaviour
{
    //TODO: Should be only working on the local client 

    public PlayerGameCharacter playerGameCharacter;


    public static AbilityTargetHandler instance;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        if (instance == null) instance = this;
    }

    public CardObject currentlySelectedCardObject;
    bool currentlyTargeting = false;
    public TargetingType targetingType;

    public IHighlightable currentlyHighlightedObject;

    public Texture2D enemyCursor, enemyHighlightCursor, freeCursor, defaulCursor;

    public enum TargetingType
    {
        Free,
        Enemy,
        none
    }

    public void SelectCard(CardObject cardObject)
    {

        currentlySelectedCardObject?.handSlot.OnCardDeSelected();
        currentlySelectedCardObject = cardObject;
        targetingType =cardObject.card.cardData.targetingType;

        currentlyTargeting = true;

        if(targetingType == TargetingType.Enemy)
        {
            Cursor.SetCursor(enemyCursor, Vector2.zero, CursorMode.Auto);
        }

        if(targetingType== TargetingType.Free)
        {
            Cursor.SetCursor(freeCursor, Vector2.zero, CursorMode.Auto);
        }
    }




    private void Update()
    {

        if (!playerGameCharacter.IsOwner) return;

        if (!currentlyTargeting) return;

        if (targetingType == TargetingType.Enemy)
        {
            LookForEnemies();
        }

        if (targetingType == TargetingType.Free)
        {
            LookForFree();
        }

    }

    public void LookForFree()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AbilityTarget abilityTarget = new AbilityTarget();
            currentlySelectedCardObject.Cast(abilityTarget);
            EndTargeting();

        }
    }
    public void LookForEnemies()
    {




        Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
        EnemyObject enemey = hit?.gameObject.GetComponent<EnemyObject>();
        if (enemey != currentlyHighlightedObject)
        {
                currentlyHighlightedObject?.UnHighLight();
                currentlyHighlightedObject = null;
                Cursor.SetCursor(enemyCursor, Vector2.zero, CursorMode.Auto);

        }

        if (hit)
        {

            Debug.Log("is over object");

           

            if (hit.transform.TryGetComponent(out EnemyObject e))
            {
                Debug.Log("is enemy");


                if (currentlyHighlightedObject != e)
                {

                    Debug.Log("isnt currently seleced");
                  
                    currentlyHighlightedObject = e;
                    Cursor.SetCursor(enemyHighlightCursor, Vector2.zero, CursorMode.Auto);
                    Debug.Log("highlighting enemy");
                    e?.Highlight();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    AbilityTarget abilityTarget = new AbilityTarget();
                    abilityTarget.enemy = e;
                    currentlySelectedCardObject.Cast(abilityTarget);
                    e?.UnHighLight();

                    EndTargeting();

                }


            }

        }

    }



    public void EndTargeting()
    {
        currentlyTargeting = false;
        Cursor.SetCursor(defaulCursor, Vector2.zero, CursorMode.Auto);

    }
}
