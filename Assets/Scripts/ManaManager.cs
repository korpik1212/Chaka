using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Image manaFiller;
    public TextMeshProUGUI manaText;

    public float currentMana = 0;
    public float maxMana = 100;



    private void FixedUpdate()
    {
        if (currentMana >= maxMana)
        {
            return;
        }
        currentMana += Time.fixedDeltaTime;
        UpdateManaUI();
    }


    public void UpdateManaUI()
    {
        manaFiller.fillAmount = currentMana / maxMana;
        manaText.text = currentMana.ToString("0.0") + "/" + maxMana.ToString("0");
    }


    public void SpendMana(float amount)
    {
        currentMana -= amount;
        UpdateManaUI();

    }

}
