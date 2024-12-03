using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Steamworks;
using UnityEngine.Events;

public class CommonNetworkManager : NetworkBehaviour
{


    public static CommonNetworkManager instance;




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




    [ServerRpc(RequireOwnership =false)]
    public void ChangeNetworkingSceneForAll(string sceneName, string[] scenesToClose)
    {
        SceneLoadData sld = new SceneLoadData(sceneName);
        NetworkConnection[] conns = instance.ServerManager.Clients.Values.ToArray();
        instance.SceneManager.LoadGlobalScenes( sld);
        instance.CloseScenes(scenesToClose);

    }

    public override void OnStopClient()
    {
        //amacýn diðer clientlara oyundan çýktýðýný bildirmek 
        //eðer hostsan diðer tüm playerlara leave lobby gönder 
        //SteamConnectionManager.instance.LeaveLobby(IsServerInitialized);
    }


    [ServerRpc(RequireOwnership = false)]
    public void CloseScenes(string[] scenesToClose)
    {
        CloseScenesObserver(scenesToClose);
    }

    [ObserversRpc]
    public void CloseScenesObserver(string[] scenesToClose)
    {
        foreach (string scene in scenesToClose)
        {

            Debug.Log("sceneUndloading");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
        }
    }

 

}
