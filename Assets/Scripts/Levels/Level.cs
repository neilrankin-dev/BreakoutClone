using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public string levelName;
    public Material levelBG;
    public AudioClip levelMusic;
}
