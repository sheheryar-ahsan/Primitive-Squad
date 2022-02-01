using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDestroy();
    }
    private void PlayerDestroy()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("Player Destroyed");
            Destroy(this.gameObject);
        }
        else if(transform.position.y <= 0)
        {
            Debug.Log("Player Destroyed");
            Destroy(this.gameObject);
            Time.timeScale = 0;
        }
    }
}
