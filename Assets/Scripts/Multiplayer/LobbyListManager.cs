using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListManager : MonoBehaviour
{



    public static LobbyListManager instance;


    public LobbyListObject lobbyListObjectPrefab;

    public GameObject lobbyGrid;

    public List<GameObject> listOfLobies = new List<GameObject>();
    Callback<LobbyMatchList_t> GameLobbyMatchList;


    public List<CSteamID> lobbyIDs = new List<CSteamID>();

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


    private void Start()
    {
        GameLobbyMatchList = Callback<LobbyMatchList_t>.Create(GetOnGetLobbyList);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetListOfLobbies();
        }
    }

    public void OpenLobbiesList()
    {
        GetListOfLobbies();
        MainMenuManager.instance.OpenLobbyList();
    }


    public void GetLobbiesList()
    {
        if (lobbyIDs.Count > 0) { lobbyIDs.Clear(); }

        SteamMatchmaking.AddRequestLobbyListStringFilter("filterTag", "15", ELobbyComparison.k_ELobbyComparisonEqual);
        SteamMatchmaking.RequestLobbyList();
    }

    private void GetOnGetLobbyList(LobbyMatchList_t param)
    {
        if (LobbyListManager.instance.listOfLobies.Count > 0) { LobbyListManager.instance.DestroyLobies(); }

        Debug.Log(param.m_nLobbiesMatching);
        for (int i = 0; i < param.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);



            lobbyIDs.Add(lobbyID);
            string lobbyName = SteamMatchmaking.GetLobbyData(lobbyID, "name");
            string hostAddress = SteamMatchmaking.GetLobbyData(lobbyID, "HostAddress");
            Debug.Log("Lobby Name: " + lobbyName + " Host Address: " + hostAddress);
            SteamMatchmaking.RequestLobbyData(lobbyID);

        }
        LobbyListManager.instance.DisplayLobies();
    }

    public void GetListOfLobbies()
    {
        GetLobbiesList();
    }
    public void DisplayLobies()
    {
        DestroyLobies();
        foreach(CSteamID lobbyID in lobbyIDs)
        {
            LobbyListObject obj= Instantiate(lobbyListObjectPrefab, lobbyGrid.transform);
            Debug.Log(SteamConnectionManager.GetPlayerAvatar(SteamMatchmaking.GetLobbyOwner(lobbyID)));
            obj.Initialize(SteamMatchmaking.GetLobbyData(lobbyID,"name"),SteamMatchmaking.GetNumLobbyMembers(lobbyID),SteamConnectionManager.GetPlayerAvatar(SteamMatchmaking.GetLobbyOwner(lobbyID)), lobbyID);
            listOfLobies.Add(obj.gameObject);
        }
        //create new lobies 
    }

    public void DestroyLobies()
    {
        foreach (GameObject lobby in listOfLobies)
        {
            Destroy(lobby);
        }
    }
}
