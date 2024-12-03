using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPManager : MonoBehaviour
{
    public float maxHealth,currentHealth;

    public Image healtbarFiller;
    public TextMeshProUGUI healthText;

    public EnemyObject enemyObject;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateVisual();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth<=0)
        {
            Death();
            currentHealth = 0;
        }
      


        UpdateVisual();


    }

    //TODO: Turn this into an event 
    public void Death()
    {
        Destroy(this.gameObject,0.5f);
        FindObjectOfType<EnemyWaveManager>().OnEnemyDefeated(enemyObject);
    }

    public void UpdateVisual()
    {
        healtbarFiller.DOFillAmount(currentHealth/ maxHealth, 0.5f);
        healthText.text= currentHealth.ToString("0");
    }
}
