using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyViewPlayerObject : MonoBehaviour
{


    public Image playerAvatar;
    public TextMeshProUGUI playerName;
    public void Initialize(PlayerClient p)
    {
        //initialize avatar
        //initialize name
        playerName.text = p.playerName;
        playerAvatar.sprite = Sprite.Create(GetSteamImageAsTexture2D(SteamFriends.GetMediumFriendAvatar(p.steamID)), new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f));
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


}
