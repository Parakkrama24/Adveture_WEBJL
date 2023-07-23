using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveLoad : MonoBehaviour
{
    [SerializeField]private Vector3 currentPose; // The current position of the player.

   // public float moveSpeed = 5f;
    //public float teleportDistance = 10f; // Distance to teleport upon pressing 'B'.

    private void Start()
    {
        // Set the initial position of the player.
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transform.position = currentPose;
            Debug.Log("Run");
        }
        /*// Regular movement logic here.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Check if the 'B' key is pressed, and update the player's position.
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Calculate the new position based on the teleport distance and the player's current position.
            Vector3 teleportPosition = currentPose + (transform.forward * teleportDistance);

            // Set the player's position to the new teleport position.
            transform.position = teleportPosition;
        }

        // Update the current pose after the regular movement.
        currentPose = transform.position;*/
    }

}
