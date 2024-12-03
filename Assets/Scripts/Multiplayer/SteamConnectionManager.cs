using FishNet.Managing;
using FishNet.Transporting;
using FishySteamworks;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SteamConnectionManager : MonoBehaviour
{
    public static SteamConnectionManager instance;



    public FishySteamworks.FishySteamworks fishySteamworks;


    Callback<LobbyCreated_t> LobbyCreated;
    Callback<LobbyEnter_t> LobbyEnter;
    Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequested;



    public static ulong CurrentLobbyID;

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
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        GameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);


    }

    public void StartMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);

    }


   
    public void JoinLobyID(CSteamID lobbyID)
    {
        Debug.Log("attemping to join loby with the ID: " + lobbyID.m_SteamID);
        if (SteamMatchmaking.RequestLobbyData(lobbyID))
        {

            SteamMatchmaking.JoinLobby(lobbyID);
        }
        else
        {
            Debug.Log("Failed to join lobby");
        }
    }

    public void CreateLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 2);
    }



    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        CurrentLobbyID = callback.m_ulSteamIDLobby;


        fishySteamworks.SetClientAddress(SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "HostAddress"));
        fishySteamworks.StartConnection(false);
    }


    public void LeaveLobby(bool isHost)
    {


        SteamMatchmaking.LeaveLobby(new CSteamID(CurrentLobbyID));
        CurrentLobbyID = 0;
        fishySteamworks.StopConnection(false);

        if (isHost) fishySteamworks.StopConnection(true);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        //Debug.Log("Starting lobby creation: " + callback.m_eResult.ToString());
        if (callback.m_eResult != EResult.k_EResultOK)
            return;
        Debug.Log(SteamUser.GetSteamID().ToString());
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        SteamMatchmaking.SetLobbyData(new CSteamID(CurrentLobbyID), "HostAddress", SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(CurrentLobbyID), "name", SteamFriends.GetPersonaName().ToString() + "'s lobby");
        SteamMatchmaking.SetLobbyData(new CSteamID(CurrentLobbyID), "filterTag", "15");
        fishySteamworks.SetClientAddress(SteamUser.GetSteamID().ToString());
        fishySteamworks.StartConnection(true);

        // StartConnectionToCurrentLobby();

        Debug.Log("Lobby created with ID: " + CurrentLobbyID.ToString());
        Debug.Log("host ID is: " + SteamUser.GetSteamID().ToString());

    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        fishySteamworks.SetClientAddress(SteamUser.GetSteamID().ToString());
        fishySteamworks.StartConnection(true);
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }







    #region static func


 

    public static Sprite GetPlayerAvatar(CSteamID playerSteamID)
    {
        // Get the large avatar handle (32x32, 64x64, or 184x184 sizes available)
        int imageHandle = SteamFriends.GetLargeFriendAvatar(playerSteamID);



        uint width, height;
        bool success = SteamUtils.GetImageSize(imageHandle, out width, out height);

        if (!success || width == 0 || height == 0)
        {
            Debug.Log("Failed to get avatar image size.");
            return null;
        }

        // Get the image RGBA data
        byte[] imageData = new byte[width * height * 4]; // RGBA = 4 bytes per pixel
        bool gotImage = SteamUtils.GetImageRGBA(imageHandle, imageData, (int)(width * height * 4));

        if (!gotImage)
        {
            Debug.LogError("Failed to get avatar image data.");
            return null;
        }

        // Create a Texture2D from the RGBA data
        Texture2D avatarTexture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
        avatarTexture.LoadRawTextureData(imageData);
        avatarTexture.Apply();

        avatarTexture = FlipTextureVertically(avatarTexture);
        Sprite avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f));
        return avatarSprite;
        // Apply the texture to the desired UI or mesh renderer (e.g., UI RawImage or 3D object)
    }
    public static Texture2D FlipTextureVertically(Texture2D original)
    {
        // Create a new texture with the same width and height as the original
        Texture2D flipped = new Texture2D(original.width, original.height);

        // Loop through all rows and flip them
        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                flipped.SetPixel(x, y, original.GetPixel(x, original.height - y - 1));
            }
        }

        // Apply the changes to the flipped texture
        flipped.Apply();

        return flipped;
    }
    #endregion
}

