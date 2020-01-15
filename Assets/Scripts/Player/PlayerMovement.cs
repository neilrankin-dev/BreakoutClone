using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 20;
    public float horizontalMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = (Input.GetAxis("Horizontal"));
        transform.Translate(Vector3.right * horizontalMovement * movementSpeed * Time.deltaTime);
    }
}
