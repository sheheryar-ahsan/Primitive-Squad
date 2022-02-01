using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bombPrefab;
    private float spawnRange = 5;
    private int enemySpawnWave = 0;
    public int enemyDestroyed = 0;
    private int bombSpawnCount = 3;
    public int bombDestroyed = 0;
    private bool nextWave = false;
    
    void Start()
    {

    }

    void Update()
    {
        SpawningTheEnemies();
        //SpawningTheBombs();
    }

    private void SpawningTheEnemies()
    {
        if (enemyDestroyed == enemySpawnWave)
        {
            enemySpawnWave++;
            Debug.Log("Enemy Wave: " + enemySpawnWave);
            for (int i = 1; i <= enemySpawnWave; i++)
            {
                GameObject temp = Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
                temp.GetComponent<Enemy>().GetSpawnManager(this.gameObject.GetComponent<SpawnManager>());
            }
            for (int i = 0; i < bombSpawnCount; i++)
            {
                GameObject temp = Instantiate(bombPrefab, GenerateRandomPosition(), bombPrefab.transform.rotation);
                temp.GetComponent<Bomb>().GetSpawnManager(this.gameObject.GetComponent<SpawnManager>());
            }
            nextWave = true;
            enemyDestroyed = 0;
        }
    }
    private Vector3 GenerateRandomPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0.8f, spawnPosZ);
        return randomPos;
    }
}
