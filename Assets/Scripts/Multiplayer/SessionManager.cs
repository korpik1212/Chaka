using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Steamworks;

public class SessionManager : NetworkBehaviour
{
    public static SessionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }



    public override void OnStartClient()
    {
        /* This is called on each client when the object
        * becomes visible to them. Networked values such as
        * Owner, ObjectId, and SyncTypes will already be
        * synchronized prior to this callback. */
        EventManager.instance.FishnetInitialized();
        Debug.Log("client started");
    }



    public readonly SyncList<PlayerClient> Clients = new SyncList<PlayerClient>();



   
    public void AssignPlayerClient(PlayerClient playerClient)
    {
        AssignPlayerClientServerRPC(playerClient);
        //do this in server instead 
    }

    [ServerRpc(RequireOwnership = false)]
    public void AssignPlayerClientServerRPC(PlayerClient playerClient)
    {
        Clients.Add(playerClient);
        AssignPlayerClientObserverRPC(playerClient);
        Debug.Log("adding player client to list, list count : " + Clients.Count);

    }
    [ObserversRpc]
    public void AssignPlayerClientObserverRPC(PlayerClient playerClient)
    {
        EventManager.instance.onPlayerAssignedToSession?.Invoke(playerClient);
    }



    public PlayerClient GetPlayerClientBySteamID(CSteamID cSteamID)
    {
        return Clients.Find(x => x.steamID == cSteamID);
    }


    public PlayerClient GetPlayerClientByNetworkConnection(NetworkConnection networkConnection)
    {
        return Clients.Find(x => x.owner == networkConnection);
    }
}
