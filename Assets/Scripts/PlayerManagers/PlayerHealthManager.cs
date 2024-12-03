using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
   
    //keep health state, maybe status and stuff too 


    public float maxHealth = 100;
    public float currentHealth = 100;

    public Image healthFiller;
    public TextMeshProUGUI healthText;
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthVisual();
    }

    public void TakeDamage(float damage)
    {
        currentHealth-= damage;

        //TODO:

        //clamp to 0 
        // if 0 die 
        UpdateHealthVisual();

    }

    public void UpdateHealthVisual()
    {
        healthFiller.DOFillAmount(currentHealth / maxHealth,0.2f);
        healthText.text = currentHealth.ToString("0.0");

    }
}
