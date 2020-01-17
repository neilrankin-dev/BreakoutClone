using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPhysicsBall : MonoBehaviour
{
    [Header("General References")]
    public GameObject resetBallInstructions;
    public float maxVelocity = 18f;

    private Rigidbody rb;
    private bool isDead = false;
    private GameManager gameManager;
    private PlaySFX playSFX;
    private bool isSprint = false;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 500);
        gameManager = FindObjectOfType<GameManager>();
        playSFX = FindObjectOfType<PlaySFX>();
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
            
            BrickStats brickStats = collision.transform.GetComponent<BrickStats>();
            if (brickStats != null)
            {
                gameManager.AddScore(brickStats.brickScoreValue);
                brickStats.DestroyBrick();
                playSFX.PlayAudioSFX(PlaySFX.Sound_HitBrick);
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
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            rb.AddForce(Vector3.up * 200);
            playSFX.PlayAudioSFX(PlaySFX.Sound_HitPaddle);

            if (playerMovement != null)
            {
                if (playerMovement.horizontalMovement >= 0.5f)
                {
                    rb.AddForce(Vector3.right * 75f);

                    if (rb.velocity.y < maxVelocity)
                    {
                        rb.AddForce(Vector3.up * 105f);
                    }
                }
                else if (playerMovement.horizontalMovement <= -0.5f)
                {
                    rb.AddForce(Vector3.right * -75f);

                    if (rb.velocity.y < maxVelocity)
                    {
                        rb.AddForce(Vector3.up * 105f);
                    }
                }
                else if (playerMovement.horizontalMovement == 0f)
                {
                    if (rb.velocity.y < maxVelocity)
                    {
                        rb.AddForce(Vector3.up * 125f);
                    }
                }
            }
        }
    
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(Vector3.up * 250);
            playSFX.PlayAudioSFX(1);
        }
    }

    private void ResetBall()
    {
        transform.SetPositionAndRotation(new Vector3(11, 0, 10), Quaternion.identity);
        rb.isKinematic = true;
    }
}
