using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] level;
    public MeshRenderer backGroundMesh;
    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        backGroundMesh.material = level[currentLevel].levelBG;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
