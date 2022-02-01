using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCursor : MonoBehaviour
{
    public float rotationSpeed = 100;
    private Transform player;
    public GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CursorMovement();
    }

    private void CursorMovement()
    {
        float horizontalInput = Input.GetAxis("Left Rotate");
        float verticalInput = Input.GetAxis("Right Rotate");

        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        }
        else if(player == null)
        {
            Destroy(this.gameObject);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
    }
}
