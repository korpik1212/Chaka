using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using UnityEngine.UI;
using FishNet.Object.Synchronizing;
using TMPro;
using FishNet.Object;

public class EnemyObject : NetworkBehaviour,IHighlightable
{
    //TODO: Turn this into a network object 

    public SpriteRenderer targetHighlighter;

    public List<Intent_SO> possibleIntents = new List<Intent_SO>();
    public Image intentImage, intentFiller;
    public TextMeshProUGUI intentValueText;

    public Intent_SO currentIntentData;
    public readonly SyncVar<float> intentCharge = new SyncVar<float>();


    public EnemyHPManager enemyHPManager;
    public class EnemyState
    {

    }

    private void Start()
    {
        if (IsServerInitialized)
        {
            DecideIntent();

        }

         intentCharge.OnChange += UpdateIntentVisual;
    }
    private void FixedUpdate()
    {
        if (IsServerInitialized)
        {
            ChargeIntent();
        }
    }

    //TODO: Decide Intent
    [ServerRpc(RequireOwnership =false)]
    public void DecideIntent()
    {
        DecideIntentObserverRPC(Random.Range(0, possibleIntents.Count));
    }
    [ObserversRpc]
    public void DecideIntentObserverRPC(int selectedIntent)
    {
        currentIntentData = possibleIntents[selectedIntent];
        UpdateIntentVisual(0,0,false);

    }

    public void ChargeIntent()
    {
        if (currentIntentData == null) return;

        intentCharge.Value += Time.fixedDeltaTime;
        if(intentCharge.Value>= currentIntentData.IntentCD)
        {
            DoIntent();
            intentCharge.Value = 0;
            DecideIntent();
        }

    }


    public void UpdateIntentVisual(float prev,float newVal,bool isOwner)
    {
        if (currentIntentData == null) return;
        intentImage.sprite= currentIntentData.intentSprite;
        intentFiller.sprite= currentIntentData.intentSprite;
        intentFiller.color= currentIntentData.intentChargeColor;
        intentFiller.fillAmount = intentCharge.Value / currentIntentData.IntentCD;
        intentValueText.text = currentIntentData.intentValue.ToString();
    }

    public void DoIntent()
    {
        if (currentIntentData.intentType == Intent_SO.IntentType.Attack)
        {
            Debug.Log("Attacking player");
            PlayerHealthManager target = TargetSelectionLogic();
            target.TakeDamage(currentIntentData.intentValue);
            
        }

    }

    public PlayerHealthManager TargetSelectionLogic()
    {
        return FindObjectOfType<PlayerHealthManager>();
    }


  

   

    #region LocalFunctions

    public void Highlight()
    {
        targetHighlighter.enabled = true;
    }
    public void UnHighLight()
    {
        if (targetHighlighter == null) return;
        targetHighlighter.enabled = false;

    }
    #endregion



   
}

