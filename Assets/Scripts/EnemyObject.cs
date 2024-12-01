using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using UnityEngine.UI;
using FishNet.Object.Synchronizing;

public class EnemyObject : MonoBehaviour,IHighlightable
{
    //TODO: Turn this into a network object 
    public EnemyState enemyState = new EnemyState();

    public SpriteRenderer targetHighlighter;

    public List<Intent_SO> possibleIntents = new List<Intent_SO>();
    public Image intentImage, intentFiller;

    public Intent_SO currentIntentData;
    public float intentCharge = 0;
    public class EnemyState
    {
        public float hp;

    }

    private void Start()
    {
        DecideIntent();
    }
    private void FixedUpdate()
    {
        ChargeIntent();
    }

    //TODO: Decide Intent
    public void DecideIntent()
    {
        // always returns attack for now
        currentIntentData = possibleIntents[Random.Range(0, possibleIntents.Count)];
        UpdateIntentVisual();
    }

    public void ChargeIntent()
    {
        intentCharge += Time.fixedDeltaTime;
        if(intentCharge>= currentIntentData.IntentCD)
        {
            DoIntent();
            intentCharge = 0;
            DecideIntent();
        }
        UpdateIntentVisual();
    }


    public void UpdateIntentVisual()
    {
        intentImage.sprite= currentIntentData.intentSprite;
        intentFiller.sprite= currentIntentData.intentSprite;
        intentFiller.color= currentIntentData.intentChargeColor;
        intentFiller.fillAmount = intentCharge / currentIntentData.IntentCD;
    }

    public void DoIntent()
    {
        if (currentIntentData.intentType == Intent_SO.IntentType.Attack)
        {
            Debug.Log("Attacking player");
        }

    }

    //serverrpc
    public void TakeDamageServerRPC(float damage)
    {

        //authoritate 
        enemyState.hp -= damage;
        //  SyncHpObserverRPC(enemyState.hp);
        OnEnemyStateChangeObserverRPC();
    }

    //observer/client rpc
    public void SyncHpObserverRPC(float hp)
    {
       enemyState.hp = hp;
       //OnEnemyStateUpdate?.invoke(enemyState);
    }

    //observeer rpc 
    public void OnEnemyStateChangeObserverRPC()
    {

    }

    #region LocalFunctions

    public void Highlight()
    {
        targetHighlighter.enabled = true;
    }
    public void UnHighLight()
    {
        targetHighlighter.enabled = false;

    }
    #endregion


    public void SyncState()
    {

    }

   
}

