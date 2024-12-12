using FishNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyWaveManager : MonoBehaviour
{
    public List<Vector3> spawnPositions = new List<Vector3>();


    List<EnemyObject> currentlySpawnedEnemyObjects = new List<EnemyObject>();
    public WaveData debugWaveData;


    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));

        StartWave(debugWaveData);

    }


    public void StartWave(WaveData waveData)
    {

        for (int i = 0; i < waveData.enemies.Count; i++)
        {
            SpawnEnemy(waveData.enemies[i], i);

        }
    }


    public void SpawnEnemy(EnemyObject enemyPrefab, int slot)
    {
        Debug.Log("eenemy spawned");
        
        EnemyObject e = Instantiate(enemyPrefab, spawnPositions[slot], Quaternion.identity);
        InstanceFinder.ServerManager.Spawn(e.gameObject);
        currentlySpawnedEnemyObjects.Add(e);
    }


    public void OnEnemyDefeated(EnemyObject e)
    {
        Debug.Log("one down : " + e.name);
        currentlySpawnedEnemyObjects.Remove(e);
        if(currentlySpawnedEnemyObjects.Count==0)
        {
            Debug.Log("Wave Cleared");
            //next wave 
        }
    }

}
