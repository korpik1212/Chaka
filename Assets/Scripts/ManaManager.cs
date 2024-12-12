using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : NetworkBehaviour
{
    public Image manaFiller;
    public TextMeshProUGUI manaText;

    public readonly SyncVar<float> currentMana = new SyncVar<float>();
    public float maxMana = 100;

    public PlayerGameCharacter PlayerGameCharacter;


    private void Start()
    {
        PlayerGameCharacter=GetComponentInParent<PlayerGameCharacter>();
        currentMana.OnChange += UpdateManaUI;
    }

    private void FixedUpdate()
    {
        if (!PlayerGameCharacter.IsOwner) return;

        if (currentMana.Value >= maxMana)
        {
            return;
        }
        currentMana.Value += Time.fixedDeltaTime;
    }


    public void UpdateManaUI(float prevVal,float newVal,bool isOwner)
    {
        manaFiller.fillAmount = currentMana.Value / maxMana;
        manaText.text = currentMana.Value.ToString("0.0") + "/" + maxMana.ToString("0");
    }


    [ServerRpc(RequireOwnership = false)]
    public void SpendManaServerRPC(float amount)
    {
        currentMana.Value -= amount;

    }

}
