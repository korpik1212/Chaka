using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTargetHandler : MonoBehaviour
{
    //TODO: Should be only working on the local client 


    public static AbilityTargetHandler instance;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        if (instance == null) instance = this;
    }

    public Card currentlySelectedCard;
    bool currentlyTargeting = false;
    public TargetingType targetingType;

    public IHighlightable currentlyHighlightedObject;
    public enum TargetingType
    {
        Free,
        Enemy,
        none
    }

    public void SelectCard(Card card)
    {
        currentlySelectedCard = card;
        targetingType = card.cardData.targetingType;
        
        currentlyTargeting = true;
        //check targeting mode 
        //
    }




    private void Update()
    {
        if (!currentlyTargeting) return;

        if (targetingType == TargetingType.Enemy)
        {
            LookForEnemies();
        }

        if (targetingType == TargetingType.Free)
        {

        }

    }

    public void LookForFree()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AbilityTarget abilityTarget = new AbilityTarget();
            currentlySelectedCard.Cast(abilityTarget);
        }
    }
    public void LookForEnemies()
    {




        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit))
        {

            Debug.Log("Clicked on: " + hit.collider.gameObject.name);

            if (hit.transform.TryGetComponent(out EnemyObject e))
            {



                if (currentlyHighlightedObject != e)
                {
                    if(currentlyHighlightedObject != null)
                    {

                        currentlyHighlightedObject.UnHighLight();
                    }
                    currentlyHighlightedObject = e;
                    e.Highlight();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    AbilityTarget abilityTarget = new AbilityTarget();
                    abilityTarget.enemy = e;
                    currentlySelectedCard.Cast(abilityTarget);
                }

               
            }

        }


      
    }

}
