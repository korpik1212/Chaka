using FishNet;
using FishNet.Connection;
using FishNet.Object;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
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
   
    public CSteamID steamID;
    public string playerName;
    public NetworkConnection owner;

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
       

        
        steamID = SteamUser.GetSteamID();
        owner = base.Owner;
        playerName= SteamFriends.GetPersonaName();
        EventManager.instance.PlayerClientInstantiated(this);
        EventManager.instance.onFishnetInitialized.AddListener(AssignToSessionManager);

       
    }



    public void AssignToSessionManager()
    {

        if (base.IsOwner)
        {
            UpdatePlayerClientStateServerRPC(this);
            SessionManager.instance.AssignPlayerClient(this);

        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerClientStateServerRPC(PlayerClient client)
    {
        UpdatePlayerClientStateObserverRPC(client);
    }

    [ObserversRpc]
    public void UpdatePlayerClientStateObserverRPC(PlayerClient client)
    {
        steamID = client.steamID;
        playerName = client.playerName;
        owner = client.owner;

        Debug.Log(client.steamID + " assigned");
        Debug.Log(client.playerName + " assigned");
        Debug.Log(client.owner + " assigned");
    }





    public override void OnStartClient()
    {
        base.OnStartClient();

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

