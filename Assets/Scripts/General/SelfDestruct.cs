using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{ 
    void Start()
    {
        Destroy(gameObject, Random.Range(1, 3));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 50);
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
        }
    }
}
