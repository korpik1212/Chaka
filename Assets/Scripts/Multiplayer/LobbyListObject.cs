using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class LobbyListObject : MonoBehaviour
{

    public TextMeshProUGUI lobbynameDisplay, playerCount;
    public Image avatar;
    public Button joinButton;


    CSteamID lobbyID;

    public void Initialize(string lobbyName,int currentPlayerCount,Sprite playerAvatar,CSteamID lobbyID)
    {
        joinButton.onClick.AddListener( ()=>SteamConnectionManager.instance.JoinLobyID(lobbyID));
        this.lobbynameDisplay.text = lobbyName;
        this.playerCount.text= currentPlayerCount.ToString()+"/4";
        this.avatar.sprite = playerAvatar;
        this.lobbyID = lobbyID;
    }


    public void OnJoin()
    {
        SteamConnectionManager.instance.JoinLobyID(lobbyID);
    }

  
}
