using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class LobyViewManager : MonoBehaviour
{

    Callback<LobbyEnter_t> LobbyEntered;

    public GameObject startButton;
    public TMPro.TextMeshProUGUI lobbyName;


    public GameObject playerObjectsGrid;
    public LobbyViewPlayerObject playerObjectPrefab;
    public List<LobbyViewPlayerObject> playerObjects = new List<LobbyViewPlayerObject>();
    private void Start()
    {
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        EventManager.instance.onPlayerClientStateUpdated.AddListener(Temp);
        EventManager.instance.onPlayerAssignedToSession.AddListener(Temp);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Temp(null);
        }
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {

        SteamConnectionManager.CurrentLobbyID = callback.m_ulSteamIDLobby;



        //   lobbyViewNetworkManagers.OnHostJoin(SteamUser.GetSteamID());
        MainMenuManager.instance.OpenLobbyView();
        CSteamID lobbyID = new CSteamID(SteamConnectionManager.CurrentLobbyID);
        startButton.SetActive(CommonNetworkManager.instance.IsServerInitialized);
        this.lobbyName.text = SteamConnectionManager.CurrentLobbyID.ToString();

    }



    public void Temp(PlayerClient playerClient)
    {
        //trigger this when someone joins the lobby
        //for each session in session manager create an object in the lobby view4
        foreach(LobbyViewPlayerObject p in playerObjects)
        {
            Destroy(p.gameObject);
        }
        playerObjects.Clear();
        foreach(PlayerClient p in SessionManager.instance.playerClients)
        {
            LobbyViewPlayerObject playerObject = Instantiate(playerObjectPrefab, playerObjectsGrid.transform);
            playerObject.Initialize(p);
            playerObjects.Add(playerObject);
        }
            
    }
}
