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
    public int maxMana = 100;



    private void FixedUpdate()
    {
        currentMana += Time.fixedDeltaTime;
        UpdateManaUI();
    }


    public void UpdateManaUI()
    {
        manaFiller.fillAmount = currentMana / maxMana;
        manaText.text = currentMana + "/" + maxMana;
    }

}
