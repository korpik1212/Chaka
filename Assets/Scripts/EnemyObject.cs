using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object.Synchronizing;

public class EnemyObject : MonoBehaviour,IHighlightable
{
    //TODO: Turn this into a network object 
    public EnemyState enemyState = new EnemyState();
    public class EnemyState
    {
        public float hp;

    }
    //serverrpc
    public void TakeDamageServerRPC(float damage)
    {

        //authoritate 
        enemyState.hp -= damage;
        //  SyncHpObserverRPC(enemyState.hp);
        OnEnemyStateChangeObserverRPC();
    }

    //observer/client rpc
    public void SyncHpObserverRPC(float hp)
    {
       enemyState.hp = hp;
       //OnEnemyStateUpdate?.invoke(enemyState);
    }

    //observeer rpc 
    public void OnEnemyStateChangeObserverRPC()
    {

    }


    public void Highlight()
    {

    }
    public void UnHighLight()
    {
    }

    public void SyncState()
    {

    }

   
}
