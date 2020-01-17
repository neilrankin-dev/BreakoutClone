using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //- 0.75 and 23
    public float movementSpeed = 20;
    public float horizontalMovement;
    public float leftScreenEdge =  -0.75f;
    public float rightScreenEdge = 23f;

    private bool isSprint = false;


    void Update()
    {
        horizontalMovement = (Input.GetAxis("Horizontal"));

        transform.Translate(Vector3.right * horizontalMovement * movementSpeed * Time.deltaTime);

        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector3(leftScreenEdge, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector3(rightScreenEdge, transform.position.y, transform.position.z);
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
            movementSpeed = 35;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprint = false;
            movementSpeed = 20;
        }

    }
}
