﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioSFX()
    {
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.Play();
    }

}