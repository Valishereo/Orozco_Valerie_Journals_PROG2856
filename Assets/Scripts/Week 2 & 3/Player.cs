using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Public and Private Variables
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public GameObject powerupPrefab;
    public GameObject aimedRocketPrefab;
    public GameObject launchedRocketPrefab;
    public List<Transform> asteroidTransforms;
    public Vector2 bombOffset;
    public float bombTrailSpacing;
    public int numberOfTrailBombs;
    public float warpRatio;
    public float radarMaxRange; // Maximum range for the radar detection
    public float maxSpeed = 2f;
    public float accelerationTime = 1f;
    public float decelerationTime = 5f;
    public float angularSpeed = 180f;
    public float radarRadius = 5f; // Radius of the radar circle
    public int radarCirclePoints = 12; // Number of points to draw the radar circle
    public float powerUpRadius = 3f; // Radius for spawning power-ups
    public int numberOfPowerUps = 6; // Number of power-ups to spawn

    private float acceleration;
    private float deceleration;
    private Vector3 velocity;
    private GameObject currentRocket;
    private Vector3 aimDirection;

    #endregion

    void Start()
    {
        //Debug.Log(NormalizeVector(new Vector3(3, 4)));
        //Debug.Log(NormalizeVector(new Vector3(-3, 4)));
        //Debug.Log(NormalizeVector(new Vector3(1.5f, -3.5f)));

        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpawnBombAtOffSet(bombOffset); // Spawn a bomb at the specified offset from the player's position when e is pressed
        }

        if (Keyboard.current.tKey.wasPressedThisFrame) // It spawns a trail of bombs behind the player when t is pressed
        {
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame) // It spawns a bomb at a random corner when r is pressed
        {
            SpawnBombOnRandomCorner(3f);
        }

        if (Keyboard.current.vKey.wasPressedThisFrame) // It warps the player towards the enemy when v is pressed
        {
            WarpPlayer(enemyTransform, warpRatio);
        }

        if (Keyboard.current.cKey.wasPressedThisFrame) // It spawns power-ups in a circular pattern around the player when c is pressed
        {
            SpawnPowerUps(powerUpRadius, numberOfPowerUps);
        }
        
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnRocket();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LaunchRocket();
        }

        PlayerMovement();

        PlayerRotation();

        DetectAsteroids(radarMaxRange, asteroidTransforms);

        EnemyRadar(radarRadius, radarCirclePoints);

        MouseAimingLine();

        AimRocket();

        
    }

    #region Player Movement
    private void PlayerMovement()
    {
        Vector3 playerInput = Vector3.zero;
        Keyboard keyboard = Keyboard.current;

        if (Keyboard.current.wKey.isPressed)
        {
            playerInput += Vector3.up;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            playerInput += Vector3.left;
            
        }

        if (Keyboard.current.dKey.isPressed)
        {
            playerInput += Vector3.right;
            
        }

        if (Keyboard.current.sKey.isPressed)
        {
            playerInput += Vector3.down;
           
        }

        if (playerInput.sqrMagnitude > 0)
        {
            velocity += acceleration * Time.deltaTime * playerInput.normalized; // Apply acceleration in the direction of the player's input
        }

        else
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

        //Debug.Log(velocity.magnitude);
    }

    private void PlayerRotation ()
    {
        if (velocity.sqrMagnitude == 0)
        {
            return; 
        }

        float deltaAngle = ComputeShortestAngle(transform.up, velocity);

        if (deltaAngle == 0)
        {
            return; 
        }

        float rotationDirection = Mathf.Sign(deltaAngle); 
        float rotionStep = angularSpeed * Time.deltaTime;

        if (rotationDirection < Mathf.Abs(deltaAngle))
        {
            transform.Rotate(0, 0, rotationDirection * rotionStep);

        }
        else
        {
            transform.Rotate(0, 0, deltaAngle);
        }
    }

    private float ComputeShortestAngle(Vector3 a, Vector3 b)
    {
        float angleA = Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg;
        float angleB = Mathf.Atan2(b.y, b.x) * Mathf.Rad2Deg;

        return Mathf.DeltaAngle(angleA, angleB);
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        if (ratio > 1) // Ensure the ratio does not exceed 1
        {
            ratio = 1;
        }

        transform.position = Vector3.Lerp(transform.position, target.position, ratio); // Move the player towards the target position based on the ratio
    }

    #endregion


    #region Bomb Spawning
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

    #endregion


    #region Detection and Radar

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

    #endregion


    #region Power-Up Spawning
    public void SpawnPowerUps(float radius, int numberOfPowerUps)
    {
        float angleStep = 360f / numberOfPowerUps; // Calculate the angle step based on the number of power-ups

        for (int i = 0; i < numberOfPowerUps; i++)
        {
            float angle = i * angleStep; // Calculate the angle for the current power-up

            float angleRadians = angle * Mathf.Deg2Rad; // Convert the angle to radians

            float x = Mathf.Cos(angleRadians) * radius;
            float y = Mathf.Sin(angleRadians) * radius;

            Vector3 spawnPosition = transform.position + new Vector3(x, y, 0); // Calculate the spawn position for the power-up

            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity); // Spawn the power-up at the calculated position
        }
    }

    #endregion

    #region Rocket Spawning

    public void MouseAimingLine()
    {
        Vector2 mouseScreenPosition =Mouse.current.position.ReadValue(); 
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = transform.position.z;

        Vector3 direction = mouseWorldPosition - transform.position; 
        direction = direction.normalized;
        aimDirection = direction;

        Vector3 enemyDirection = enemyTransform.position - transform.position; 

        float angles = Vector3.Angle(direction, enemyDirection); // Calculate the angle between the player-to-mouse direction and the player-to-enemy direction

        Color lineColor = Color.yellow; 
        if (angles <=5f) // If the angle is less than or equal to 5 degrees, change the line color to red
        {
            lineColor = Color.red;
        }

        Vector3 endPosition = transform.position + direction * 3f; 

        Debug.DrawLine(transform.position, endPosition, lineColor); 
    }

    public void SpawnRocket()
    {
        if (currentRocket == null) // Check if there is no existing rocket
        {
            Vector3 spawnPosition = transform.position + aimDirection * 2f;
            currentRocket = Instantiate(aimedRocketPrefab, spawnPosition, Quaternion.identity); // Spawn a new rocket at the player's position
        }
    }

    public void AimRocket()
    {
        if (currentRocket != null)
        {
            Vector3 rocketPosition = transform.position + aimDirection * 2f;
            currentRocket.transform.position = rocketPosition;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // Calculate the angle based on the aim direction)
            angle -= 90f; // Adjust the angle to align with the rocket's orientation
            currentRocket.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

    }

    public void LaunchRocket()
    {
        if (currentRocket != null)
        {
            Vector3 launchPosition = currentRocket.transform.position;
            Quaternion launchRotation = currentRocket.transform.rotation;

            //Instantiate(launchedRocketPrefab, launchPosition, launchRotation);

            GameObject launchedRocket = Instantiate(launchedRocketPrefab, launchPosition, launchRotation);

            Rocket rocketScript = launchedRocket.GetComponent<Rocket>(); // Get the Rocket script component from the launched rocket
            rocketScript.enemyTransform = enemyTransform; // Assign the enemyTransform to the launched rocket
            rocketScript.Launch();

            Debug.Log(rocketScript);

            Destroy(currentRocket);
            currentRocket = null;
        }

    }
    #endregion

    #region Utility Functions
    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector;
    }

    #endregion

}
