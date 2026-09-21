using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset;
    public float bombTrailSpacing;
    public int numberOfTrailBombs;

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector3(3, 4)));
        Debug.Log(NormalizeVector(new Vector3(-3, 4)));
        Debug.Log(NormalizeVector(new Vector3(1.5f, -3.5f)));
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
            {
            SpawnBombAtOffSet(bombOffset); // Spawn a bomb at the specified offset from the player's position
            }

        if (Keyboard.current.tKey.wasPressedThisFrame) // It spawns a trail of bombs behind the player when T is pressed
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }
    }
    public void SpawnBombAtOffSet(Vector3 inOffset)
    {
        Vector3 playerPos = transform.position;
        Vector3 spawnPos = playerPos + inOffset;

        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }

    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs) 
    {
        for (int i = 1; i <= inNumberOfBombs; i++) // It start at 1 to avoid spawning a bomb at the player's position
        {
            float distance = inBombSpacing * i; // To calculate the distance for each bomb in the trail
            Vector3 offset = -transform.up * distance; 
            SpawnBombAtOffSet(offset);
        }
    }

    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector;
    }
}
