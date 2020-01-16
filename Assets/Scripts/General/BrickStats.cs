using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStats : MonoBehaviour
{
    public int brickScoreValue;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void DestroyBrick()
    {
        ExtraLifeChance();
        gameManager.RemoveBrick();
    }

    void ExtraLifeChance()
    {
        int chance = Random.Range(0, 35);
        if (chance == 1)
        {
            gameManager.AddLife();
        }
    }

}
