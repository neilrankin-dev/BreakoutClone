using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{


    AudioSource audioSource;

    public AudioClip[] hitSounds;  // 0 - brickHit, 1 - paddleHit

    public const int Sound_HitBrick = 0;
    public const int Sound_HitPaddle = 1;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioSFX(int soundId)
    {
        audioSource.clip = hitSounds[soundId];
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.Play();
    }

}
