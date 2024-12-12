using DG.Tweening;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPManager : NetworkBehaviour
{
    public float maxHealth;

    public readonly SyncVar<float> currentHealth=new SyncVar<float>();
    public Image healtbarFiller;
    public TextMeshProUGUI healthText;

    public EnemyObject enemyObject;

    private void Start()
    {
        currentHealth.OnChange+= UpdateVisual;
        if (IsServerInitialized)
        {
            currentHealth.Value = maxHealth;

        }


    }

    private void Update()
    {
        if (IsServerInitialized)
        {
            currentHealth.Value = currentHealth.Value;
        }
    }


    [ServerRpc(RequireOwnership =false)]
    public void TakeDamageServerRPC(float damage)
    {
        currentHealth.Value -= damage;
        

        if(currentHealth.Value<=0)
        {
            //death should also be an observer rpc
            DeathObserverRPC();
            currentHealth.Value = 0;
        }
      


    }




    public void DeathObserverRPC()
    {
        Destroy(this.gameObject, 0.5f);
        FindObjectOfType<EnemyWaveManager>().OnEnemyDefeated(enemyObject);
    }

    public void UpdateVisual(float prev, float next, bool asServer)
    {
        healtbarFiller.DOFillAmount(currentHealth.Value/ maxHealth, 0.5f);
        healthText.text= currentHealth.Value.ToString("0");
    }
}
