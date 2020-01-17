using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPhysicsBall : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject resetBallInstructions;
    public float maxVelocity = 18f;

    bool isDead = false;
    PlaySFX playSFX;
    GameManager gameManager;
    // 0 - brickHit, 1 - paddleHit

 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 500);
        playSFX = FindObjectOfType<PlaySFX>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                resetBallInstructions.SetActive(false);
                isDead = false;
                rb.isKinematic = false;
                rb.AddForce(Vector3.down * 500);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            rb.AddForce(Vector3.down * 100f);
            playSFX.PlayAudioSFX(0);
            BrickStats brickStats = collision.transform.GetComponent<BrickStats>();
            if (brickStats != null)
            {
                gameManager.AddScore(brickStats.brickScoreValue);
                brickStats.DestroyBrick();

            }
            Destroy(collision.gameObject, 0.1f);
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            gameManager.SubtractLife();
            resetBallInstructions.SetActive(true);
            isDead = true;
            ResetBall();
        }

        if (collision.gameObject.CompareTag("SideWall"))
        {
            rb.AddForce(Vector3.up * 100);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up * 150);
            playSFX.PlayAudioSFX(1);
        }
    
    }

    private void ResetBall()
    {
        transform.SetPositionAndRotation(new Vector3(11, 0, 10), Quaternion.identity);
        rb.isKinematic = true;
    }
}
