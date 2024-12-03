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


    public UnityEvent<PlayerClient> onPlayerClientInstantiated;
    public UnityEvent onFishnetInitialized;
    public UnityEvent<PlayerClient> onPlayerAssignedToSession;

    public void PlayerClientInstantiated(PlayerClient playerClient)
    {
        onPlayerClientInstantiated.Invoke(playerClient);
    }

    public void FishnetInitialized()
    {

       onFishnetInitialized.Invoke();
    }

    public void PlayerAssignedToSession(PlayerClient playerClient)
    {
        onPlayerAssignedToSession.Invoke(playerClient);
    }


}

    

