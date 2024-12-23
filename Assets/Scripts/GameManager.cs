using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : NetworkBehaviour
{

    

    public PlayerGameCharacter playerGameCharacter1, playerGameCharacter2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        AssignClientToGameCharacter();

        if (IsServerInitialized)
        {
            playerGameCharacter1.abilityTargetHandler.Activate();
        }

        else
        {
            playerGameCharacter2.abilityTargetHandler.Activate();

        }

    }


    public void AssignClientToGameCharacter()
    {
        playerGameCharacter1.GiveOwnership(SessionManager.instance.playerClients[0].owner);


        playerGameCharacter1.OwnerAssigned(SessionManager.instance.playerClients[0]);

        if (SessionManager.instance.playerClients.Count > 1)
        {
            playerGameCharacter2.GiveOwnership(SessionManager.instance.playerClients[1].owner);
            playerGameCharacter2.OwnerAssigned(SessionManager.instance.playerClients[1]);


        }

    }
}
