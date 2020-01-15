using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("General References")]
    public GameObject defaultBrick;
    public TextMeshProUGUI scoreText;
    public int playerScore;
    public int brickID;

    [Header("Material References")]
    public Material[] brickMaterials;
  
    // Start is called before the first frame update
    void Start()
    {
        SetupBricks();
    }

    void SetupBricks()
    {
        float x = 0;
        float y = 5;
        for (int i = 0; i < 6; i++)
        {
            brickID = i + 1;
            for (int s = 0; s < 14; s++)
            {
                Vector3 currentPos = new Vector3(x, y, 10);
                SpawnBrick(currentPos, brickMaterials[i], brickID);
                x += 1.75f;
            }
            x = 0;
            y -= 0.5f;
        }
    }

    void SpawnBrick(Vector3 brickPos, Material curMaterial, int idOfBrick)
    {
        GameObject currentBrick = Instantiate(defaultBrick, null) as GameObject;
        currentBrick.transform.SetPositionAndRotation(brickPos, Quaternion.identity);
        MeshRenderer currentBrickMat = currentBrick.GetComponent<MeshRenderer>();
        currentBrickMat.material = curMaterial;
        BrickStats brickStats = currentBrick.GetComponent<BrickStats>();
        if (brickStats != null)
        {
            switch (idOfBrick)
            {
                case 1:
                    brickStats.brickScoreValue = 150;
                    break;
                case 2:
                    brickStats.brickScoreValue = 125;
                    break;
                case 3:
                    brickStats.brickScoreValue = 100;
                    break;
                case 4:
                    brickStats.brickScoreValue = 75;
                    break;
                case 5:
                    brickStats.brickScoreValue = 50;
                    break;
                case 6:
                    brickStats.brickScoreValue = 25;
                    break;

            }


            
        }
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        scoreText.text = "Score: " + playerScore;
    }
}
