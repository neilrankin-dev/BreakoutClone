using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("General References")]
    public GameObject defaultBrick;
    public int playerScore;
    public int brickID;
    public int playerLives = 4;

    public bool isPaused = false;

    [Header("Material References")]
    public Material[] brickMaterials;

    [Header("UI References")]
    public GameObject playerLife;
    public GameObject gameOverText;
    public TextMeshProUGUI scoreText;
    public List<GameObject> playerLifeIcons = new List<GameObject>();
    public GameObject pauseMenu;

    private bool isGameOver = false;
 
  
    // Start is called before the first frame update
    void Start()
    {
        SetupBricks();
        SetupPlayerLives(playerLives);
    }

    private void Update()
    {
        if (isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameOverText.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            ResumeGame();
        }

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

    public void SetupPlayerLives(int amount)
    {
        for (int i = 0; i < playerLives; i++)
        {
            GameObject playerLifeOBJ = Instantiate(playerLife, null) as GameObject;
            Vector3 playerLifePos = new Vector3(i + 0.65f, 6.25f, 10f);
            playerLifeOBJ.transform.SetPositionAndRotation(playerLifePos, Quaternion.identity);
            playerLifeIcons.Add(playerLifeOBJ as GameObject);
        }
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        scoreText.text = "Score: " + playerScore;
    }


    public void SubtractLife()
    {
        playerLives -= 1;
        Destroy(playerLifeIcons[playerLives].gameObject);
        playerLifeIcons.Remove(playerLifeIcons[playerLives]);

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverText.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ExitGame()
    {
        print("Quit game!!");
        Application.Quit();
    }

}
