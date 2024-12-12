using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using TMPro;
public class PlayerGameCharacter : NetworkBehaviour
{
    public TextMeshProUGUI nameText;


    public void OwnerAssigned(PlayerClient playerClient)
    {
        nameText.text = playerClient.playerName;
    }
}
