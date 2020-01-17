using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStats : MonoBehaviour
{
    public int brickScoreValue;

    private GameManager gameManager;
    private MeshRenderer meshRenderer;
    private bool containsExtraLife;
    private GameObject extraLifeBrick;

    [Header("Explosion Settings")]
    public float cubeSize = 0.1f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();

        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        ExtraLifeChance();
    }

    public void DestroyBrick()
    {
        Destroy(extraLifeBrick);
        Explode();

        if (containsExtraLife)
        {
            gameManager.AddLife();
            //play 1up sound
        }

        gameManager.RemoveBrick();
    }

    void ExtraLifeChance()
    {
        int chance = Random.Range(0, 35);
        if (chance == 1)
        {
            containsExtraLife = true;
            extraLifeBrick = Instantiate(gameManager.powerupBrick, null) as GameObject;
            extraLifeBrick.GetComponent<MeshRenderer>().material = gameManager.extraLifeMaterial;
            extraLifeBrick.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        }
    }

    public void Explode()
    {
        //make object disappear
        //gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

    }

    void createPiece(int x, int y, int z)
    {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        piece.GetComponent<MeshRenderer>().material = meshRenderer.material;
        piece.AddComponent<SelfDestruct>();
    }

}
