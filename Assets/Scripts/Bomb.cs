using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody bombRb;
    private Player player;
    private Transform cursor;
    public ParticleSystem bombExplosion;
    private GameObject playerGameObjectRefered;
    private GameObject enemyGameObjectRefered;
    private SpawnManager spawnManager;
    private bool bombPicked = false;
    private bool thrownCheck = true;
    private float timeCounter = 0;
    private bool counterCheck = false;
    private bool bombExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        bombRb = GetComponent<Rigidbody>();
        //cursor = cursorObject.GetComponent<Transform>();
        cursor = GameObject.FindGameObjectWithTag("Focal Point").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bombExplosion.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        BombCollected();
        ThrowingBomb();
        if (counterCheck == true)
        {
            timeCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && bombPicked == false && timeCounter <= 0)
        {
            bombPicked = true;
            Debug.Log("Explosion Bomb Collected");
            thrownCheck = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void BombCollected()
    {
        if (bombPicked == true && thrownCheck == false && cursor != null)
        {
            transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y + 0.37f, cursor.transform.position.z);
            bombRb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void ThrowingBomb()
    {
        if (Input.GetKeyDown(KeyCode.F) && bombPicked == true)
        {
            bombPicked = false;
            thrownCheck = true;
            bombRb.constraints = RigidbodyConstraints.None;
            bombRb.AddForce(Vector3.up.normalized * 10, ForceMode.Impulse);
            //bombRb.AddForce(Vector3.forward * 3, ForceMode.Impulse);
            timeCounter = 3f;
            counterCheck = true;
            StartCoroutine(BombExploded());
        }
    }

    IEnumerator BombExploded()
    {
        yield return new WaitForSeconds(3);
        bombExploded = true;
        bombExplosion.gameObject.SetActive(true);
        bombExplosion.Play();
        spawnManager.bombDestroyed++;
        Debug.Log("Bomb Exploded");
        StartCoroutine(DestroyBomb());
    }
    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyGettingReference(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerGettingReference(other.gameObject);
        }

        if (bombExploded == true && playerGameObjectRefered != null && enemyGameObjectRefered == null)
        {
            player.playerHealth -= 100;
            Debug.Log("Player Health: " + player.playerHealth);
            bombExploded = false;
        }
        else if (bombExploded == true && enemyGameObjectRefered != null && playerGameObjectRefered == null)
        {
            enemyGameObjectRefered.GetComponent<Enemy>().enemyHealth -= 100;
            bombExploded = false;
        }
        else if (bombExploded == true && playerGameObjectRefered != null && enemyGameObjectRefered != null)
        {
            player.playerHealth -= 100;
            enemyGameObjectRefered.GetComponent<Enemy>().enemyHealth -= 100;
            bombExploded = false;
        }
    }
    public void EnemyGettingReference(GameObject gameObjectReferedIs)
    {
        enemyGameObjectRefered = gameObjectReferedIs;
    }
    public void PlayerGettingReference(GameObject gameObjectReferedIs)
    {
        playerGameObjectRefered = gameObjectReferedIs;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyGameObjectRefered = null;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerGameObjectRefered = null;
        }
    }
    public void GetSpawnManager(SpawnManager spawnManagerObject)
    {
        spawnManager = spawnManagerObject;
    }
}
