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

    public static Texture2D GetSteamImageAsTexture2D(int iImage)
    {
        Texture2D ret = null;
        uint ImageWidth;
        uint ImageHeight;
        bool bIsValid = SteamUtils.GetImageSize(iImage, out ImageWidth, out ImageHeight);

        if (bIsValid)
        {
            byte[] Image = new byte[ImageWidth * ImageHeight * 4];

            bIsValid = SteamUtils.GetImageRGBA(iImage, Image, (int)(ImageWidth * ImageHeight * 4));
            if (bIsValid)
            {
                ret = new Texture2D((int)ImageWidth, (int)ImageHeight, TextureFormat.RGBA32, false, true);
                ret.LoadRawTextureData(Image);
                ret.Apply();
            }
        }

        return ret;
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
}
