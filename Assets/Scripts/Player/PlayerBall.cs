using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public GameObject resetBallInstructions;
    public float maxVelocity = 18f;

    Rigidbody rb;
    bool isDead = false;
    PlaySFX playSFX;
    GameManager gameManager;
    // 0 - brickHit, 1 - paddleHit

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playSFX = FindObjectOfType<PlaySFX>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {      

       // print(rb.velocity);
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                resetBallInstructions.SetActive(false);
                isDead = false;
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
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

        if (collision.gameObject.CompareTag("Player"))
        {
            playSFX.PlayAudioSFX(1);
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            //print("Collide with Player!!");


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

            if (rb.useGravity)
            {
                rb.useGravity = false;
            }

        }

        if (collision.gameObject.CompareTag("Death"))
        {
            gameManager.SubtractLife();
            resetBallInstructions.SetActive(true);
            isDead = true;
            ResetBall();
        }

    }

    void ResetBall()
    {
        transform.SetPositionAndRotation(new Vector3(11, 0, 10), Quaternion.identity);
        rb.isKinematic = true;
    }


}
