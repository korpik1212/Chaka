using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    public static EventManager instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Header("runs x^2 times")]
    public UnityEvent<PlayerClient> onPlayerClientInstantiated;
    public UnityEvent<PlayerClient> onPlayerAssignedToSession;
    public UnityEvent<PlayerClient> onPlayerClientStateUpdated;
    public UnityEvent onFishnetInitialized;
   

    public void PlayerClientInstantiated(PlayerClient playerClient)
    {
        onPlayerClientInstantiated?.Invoke(playerClient);
    }

    public void FishnetInitialized()
    {

       onFishnetInitialized?.Invoke();
    }

    public void PlayerAssignedToSession(PlayerClient playerClient)
    {
        onPlayerAssignedToSession?.Invoke(playerClient);
    }


    public void PlayerClientStateUpdated(PlayerClient playerClient)
    {
        onPlayerClientStateUpdated?.Invoke(playerClient);
    }

}

    

