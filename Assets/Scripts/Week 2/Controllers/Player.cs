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
    public float warpRatio;
    public float radarMaxRange; // Maximum range for the radar detection
    public float maxSpeed = 1f;
    public float accelerationTime = 1f;

    private float acceleration;
    private Vector3 velocity;

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector3(3, 4)));
        Debug.Log(NormalizeVector(new Vector3(-3, 4)));
        Debug.Log(NormalizeVector(new Vector3(1.5f, -3.5f)));

        acceleration = maxSpeed / accelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SpawnBombAtOffSet(bombOffset); // Spawn a bomb at the specified offset from the player's position when B is pressed
        }

        if (Keyboard.current.tKey.wasPressedThisFrame) // It spawns a trail of bombs behind the player when T is pressed
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame) // It spawns a bomb at a random corner when R is pressed
        {
            SpawnBombOnRandomCorner(3f);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame) // It warps the player towards the enemy when W is pressed
        {
            WarpPlayer(enemyTransform, warpRatio);
        }

        PlayerMovement();

        DetectAsteroids(radarMaxRange, asteroidTransforms); 
    }

   private void PlayerMovement()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.up;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.left;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.right;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.down;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        Debug.Log(velocity.magnitude);
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

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        int randomCorner = Random.Range(0, 4); // Randomly select a corner 
        Vector3 direction = Vector3.zero;
        
        switch (randomCorner) 
        {
            case 0:
                direction = transform.up + transform.right; // Top-right corner
                break;
            case 1:
                direction = transform.up - transform.right; // Top-left corner
                break;
            case 2:
                direction = -transform.up + transform.right; // Bottom-right corner
                break;
            case 3:
                direction = -transform.up - transform.right; // Bottom-left corner
                break;
        }

        direction =NormalizeVector(direction);
        Vector3 offset = direction * inDistance; // Calculate the offset based on the distance and direction
        SpawnBombAtOffSet(offset); // Spawn the bomb at the calculated offset
    }

    public void WarpPlayer(Transform target, float ratio) 
    { 
        if (ratio > 1) // Ensure the ratio does not exceed 1
        {
            ratio = 1;
        }

        transform.position = Vector3.Lerp(transform.position, target.position, ratio); // Move the player towards the target position based on the ratio
    }

    public void DetectAsteroids(float inMaxRange, List <Transform> inAsteroid)
    {
        for (int i = 0; i < inAsteroid.Count; i++) // Iterate through the list of asteroids
        {
            Transform asteroid = inAsteroid[i];
            float distance = Vector3.Distance(transform.position, asteroid.position); // Calculate the distance between the player and the asteroid

            if (distance <= inMaxRange) 
            {
                Vector3 direction = asteroid.position - transform.position;
                direction = direction.normalized; 
                Vector3 endPosition = transform.position + direction * 2.5f; // Calculate the end position for the raycast

                Debug.DrawLine(transform.position, endPosition, Color.green); // Draw a green line to indicate the detection of the asteroid
            }
        }
    }

    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector;
    }
}
