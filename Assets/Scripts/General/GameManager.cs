using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("General References")]
    public static int playerScore;
    public int brickID;
    public int playerLives = 4;

    public bool isPaused = false;

    [Header("Material and brick References")]
    public Material[] brickMaterials;
    public GameObject defaultBrick;
    public int brickCount = 0;

    [Header("Powerup Material References")]
    public Material extraLifeMaterial;
    public GameObject powerupBrick;
    

    [Header("UI References")]
    public GameObject playerLife;
    public GameObject gameOverText;
    public GameObject youWinText;
    public TextMeshProUGUI scoreText;
    public List<GameObject> playerLifeIcons = new List<GameObject>();
    public GameObject pauseMenu;

    private bool isGameOver = false;

    void Awake()
    {
        SetupBricks();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupPlayerLives(playerLives);

        PauseGame();
    }

    private void Reset()
    {
        brickCount = 0;
        SetupBricks();
        playerLives = 5;
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
                PlayerBall playerBall = FindObjectOfType<PlayerBall>();
                Rigidbody playerBallRB = playerBall.GetComponent<Rigidbody>();
                playerScore = 0;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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
                brickCount += 1;
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

    public void AddLife()
    {
        playerLives += 1;
        GameObject playerLifeOBJ = Instantiate(playerLife, null) as GameObject;
        Vector3 playerLifePos = new Vector3(playerLives - 1 + 0.65f, 6.25f, 10f);
        playerLifeOBJ.transform.SetPositionAndRotation(playerLifePos, Quaternion.identity);
        playerLifeIcons.Add(playerLifeOBJ as GameObject);
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

    public void RemoveBrick()
    {
        brickCount -= 1;
        if (brickCount <= 0)
        {
            WinLevel();
        }
    }

    public void WinLevel()
    {
        isGameOver = true;
        Time.timeScale = 0;

        if (youWinText != null)
        {
            youWinText.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        for (int i = 0; i < playerLives; i++)
        {
            Destroy(playerLifeIcons[i].gameObject);
        }


        playerLifeIcons.Clear();
    }

}
