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
    public float maxSpeed = 2f;
    public float accelerationTime = 1f;
    public float decelerationTime = 5f;
    public float radarRadius = 5f; // Radius of the radar circle
    public int radarCirclePoints = 12; // Number of points to draw the radar circle

    private float acceleration;
    private float deceleration;
    private Vector3 velocity;

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector3(3, 4)));
        Debug.Log(NormalizeVector(new Vector3(-3, 4)));
        Debug.Log(NormalizeVector(new Vector3(1.5f, -3.5f)));

        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
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

        EnemyRadar(radarRadius, radarCirclePoints);
    }

   private void PlayerMovement()
    {
        bool isMoving = false; // Flag to check if the player is moving

        if (Keyboard.current.upArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.up;
            isMoving = true;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.left;
            isMoving = true;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.right;
            isMoving = true;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            velocity += acceleration * Time.deltaTime * Vector3.down;
            isMoving = true;
        }

        if (!isMoving) 
        {
           float decelerationThisFrame = deceleration * Time.deltaTime;

            if (velocity.magnitude <= decelerationThisFrame)
            {
                velocity = Vector3.zero; // Stops the player completely if the velocity is less than or equal to the deceleration for this frame
            }
            else
            {
                velocity -= velocity.normalized * decelerationThisFrame; // Apply deceleration in the opposite direction of the current velocity
            }

            //velocity -= velocity.normalized * deceleration * Time.deltaTime;
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

    public void EnemyRadar(float radius, int circlePoints)
    { 
        float angleStep = 360f / circlePoints; // Calculate the angle step based on the number of points

        float enemyDistance = Vector3.Distance(transform.position, enemyTransform.position); // Calculate the distance between the player and the enemy
        Color radarColor = Color.green; // Default color for the radar circle

        if (enemyDistance <= radius)
        {
            radarColor = Color.red;
        }
        
        for (int i = 0; i < circlePoints; i++) 
        {
            float angle = i * angleStep; // Calculate the angle for the current point

            float angleRadians = angle * Mathf.Deg2Rad; // Convert the angle to radians

            float x = Mathf.Cos(angleRadians) * radius; 
            float y = Mathf.Sin(angleRadians) * radius; 

            Vector3 point = transform.position + new Vector3(x, y, 0); 

            float nextAngle = (i + 1) * angleStep;
            float nextAngleRadians = nextAngle * Mathf.Deg2Rad;

            float nextX = Mathf.Cos(nextAngleRadians) * radius;
            float nextY = Mathf.Sin(nextAngleRadians) * radius;

            Vector3 nextPoint = transform.position + new Vector3(nextX, nextY, 0);

            Debug.DrawLine(point, nextPoint, radarColor); 
        }

    }
    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector;
    }
}
