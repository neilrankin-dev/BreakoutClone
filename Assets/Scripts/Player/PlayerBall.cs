using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public GameObject resetBallInstructions;

    Rigidbody rb;
    bool isDead = false;
    PlaySFX playSFX;
    GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playSFX = FindObjectOfType<PlaySFX>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                resetBallInstructions.SetActive(false);
                isDead = false;
                rb.isKinematic = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            playSFX.PlayAudioSFX();
            BrickStats brickStats = collision.transform.GetComponent<BrickStats>();
            if (brickStats != null)
            {
                gameManager.AddScore(brickStats.brickScoreValue);
            }
            Destroy(collision.gameObject, 0.1f);           
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            print("Collide with Player!!");
            print(rb.velocity);

            if (playerMovement != null)
            {
                if (playerMovement.horizontalMovement >= 0.5f)
                {
                    rb.AddForce(Vector3.right * 75f);
                    rb.AddForce(Vector3.up * 105f);
                }
                else if (playerMovement.horizontalMovement <= -0.5f)
                {
                    rb.AddForce(Vector3.right * -75f);
                    rb.AddForce(Vector3.up * 105f);
                }
                else if (playerMovement.horizontalMovement == 0f)
                {
                    rb.AddForce(Vector3.up * 100f);
                }
            }

        }

        if (collision.gameObject.CompareTag("Death"))
        {
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
