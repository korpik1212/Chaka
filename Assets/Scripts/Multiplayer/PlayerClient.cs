using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerClient : NetworkBehaviour
{
    /*
    [HideInInspector]
    public PlayerCharacter currentPlayerCharacter;
    public PlayerCharacter playerCharacterPrefab;
    public LobbyViewObject currentLobbyViewObject;
    */






    /*

    public PlayerClientState playerClientState;
    public class PlayerClientState
    {
        public PlayerClientSteamData steamData;
        public PlayerClientGameData gameData;

        public bool isDirty;
    }

    */

    public TextMeshProUGUI clientData;

    public CSteamID steamID;
    public string playerName;
    public NetworkConnection owner;

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {



        //you only set at the owner client but try to feed in the data from the server client, thats a no no 

        //so then that was the problem
        //local data 
        
        if (base.IsOwner)
        {
            steamID = SteamUser.GetSteamID();
            owner = base.Owner;
            playerName = SteamFriends.GetPersonaName();
            Debug.Log("I am the owner of: " + playerName);

            string s = "";
            s+=steamID.ToString()+"\n";
            s+=playerName+"\n";
            s+=owner.ClientId.ToString()+"\n";
            clientData.text = s;

            SetPlayerClientDataServerRPC(steamID,owner,playerName);


             EventManager.instance.onFishnetInitialized.AddListener(AssignToSessionManager);

        }


        if (!base.IsOwner && !IsServerInitialized)
        {
            UpdatePlayerClientStateServerRPC();
        }

        EventManager.instance.PlayerClientInstantiated(this);

    }





    public override void OnStartClient()
    {
        base.OnStartClient();


    }

    public void AssignToSessionManager()
    {
        SessionManager.instance.AssignPlayerClient(this);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerClientDataServerRPC(CSteamID steamID,NetworkConnection owner,string playerName)
    {
        this.steamID = steamID;
        this.owner = owner;
        this.playerName = playerName;


        UpdatePlayerClientStateServerRPC();
       
    }


    [ServerRpc(RequireOwnership =false)]
    public void UpdatePlayerClientStateServerRPC()
    {
        UpdatePlayerClientStateObserverRPC(steamID,owner,playerName);
    }



    [ObserversRpc]
    public void UpdatePlayerClientStateObserverRPC(CSteamID steamID, NetworkConnection owner, string playerName)
    {
        this.steamID = steamID;
        this.playerName = playerName;
        this.owner = owner;

        EventManager.instance.PlayerClientStateUpdated(this);
        Debug.Log(steamID + " UPDATED");
        Debug.Log(playerName + " UPDATED");
    }





   













    /*
    [ObserversRpc]
    public void UpdateStateObserverRPC(PlayerClientState playerClientState, NetworkConnection requestOwner)
    {
        EventManager.Instance.PlayerClientStateChanged(new EventManager.PlayerClientStateChangeCallback() { playerClient = this, owner = this.Owner });

    }
    */

    /*
    public void SpawnGameBody()
    {

        PlayerCharacter playerBody = Instantiate(playerCharacterPrefab, SpawnPointSystem.instance.GetSpawnPoint(team).position, Quaternion.identity);
        Debug.Log(playerBody.GetComponentInChildren<LaserShot>().projectileSpeed.ToString()+"proj");
        Spawn(playerBody.gameObject, base.Owner);
        SetClients(playerBody, this);
    }
    */


}

