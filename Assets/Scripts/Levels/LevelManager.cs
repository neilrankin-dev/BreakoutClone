using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("General References")]
    public Level[] level;
    public MeshRenderer backGroundMesh;
    public int currentLevel = 0;
    public AudioSource musicAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        backGroundMesh.material = level[currentLevel].levelBG;
        musicAudioSource.clip = level[currentLevel].levelMusic;
        musicAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
