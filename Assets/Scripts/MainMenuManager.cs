using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager instance;



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


    public TMP_InputField lobbyInput;
    public GameObject starterPanel, lobbyPanel, lobbyList;
    public void CreateLobby()
    {

        SteamConnectionManager.instance.CreateLobby();
    }

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));

    }

    public void JoinLobyInputField()
    {
        CSteamID lobbyID = new CSteamID(Convert.ToUInt64(lobbyInput.text));
        SteamConnectionManager.instance.JoinLobyID(lobbyID);

    }

    public void StartGame()
    {
        string[] scenesToClose= new string[] { "MainMenu" };
        CommonNetworkManager.instance.ChangeNetworkingSceneForAll("GameScene",scenesToClose);
    }

    public void OpenLobbyView()
    {
        lobbyPanel.SetActive(true);
        starterPanel.SetActive(false);
        lobbyList.SetActive(false);

    }

    public void OpenLobbyList()
    {
        lobbyPanel.SetActive(false);
        starterPanel.SetActive(false);
        lobbyList.SetActive(true);
    }

    public void OpenStartPanel()
    {
        lobbyPanel.SetActive(false);
        starterPanel.SetActive(true);
        lobbyList.SetActive(false);

    }   

}
