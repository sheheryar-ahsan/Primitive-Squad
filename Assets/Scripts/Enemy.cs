using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;
    public float movingSpeed;
    public float enemyHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAI();
        EnemyDestroy();
    }
    private void EnemyAI()
    {
        if (player != null)
        {
            enemyRb.AddForce((player.transform.position - transform.position).normalized * movingSpeed * Time.deltaTime);
        }
    }
    private void EnemyDestroy()
    {
        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy Destroyed");
            spawnManager.enemyDestroyed++;
            Debug.Log(spawnManager.enemyDestroyed);
            Destroy(this.gameObject);
        }
        else if(transform.position.y <= 0)
        {
            spawnManager.enemyDestroyed++;
            Destroy(this.gameObject);
        }
    }

    public void GetSpawnManager(SpawnManager spawnManagerObject)
    {
        spawnManager = spawnManagerObject;
    }
}
