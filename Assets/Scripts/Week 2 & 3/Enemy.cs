using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange; // Range within which the enemy can detect the player
    public float moveSpeed;
    public float decelerationTime = 5f; // Time it takes for the enemy to decelerate to a stop
    public float respawnDelay = 3f;
    public float blinkDuration = 2f; // Duration of the blink effect when the enemy is defeated
    public float blinkInterval = 0.2f; // Interval between blinks when the enemy is defeated

    private Vector3 velocity;
    private Vector3 initialPosition;
    private bool isDefeated = false;

    private void Start()
    {
        initialPosition = transform.position; // Store the initial position of the enemy

    }
    private void Update()
    {
        if (!isDefeated)
        {
            EnemyMovement();
        }
    }

    public void EnemyMovement()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < detectionRange)
        {
            Vector3 direction = playerTransform.position - transform.position; // Calculate the direction towards the player
            direction = direction.normalized;

            //transform.position += direction * moveSpeed * Time.deltaTime;

            velocity = direction * moveSpeed;


            Debug.Log("Player has been detected");
        }
        else
        {
            float deceleration = moveSpeed / decelerationTime;
            float decelerationThisFrame = deceleration * Time.deltaTime;

            if (velocity.magnitude <= decelerationThisFrame)
            {
                velocity = Vector3.zero; // Stop the enemy completely if the velocity is less than or equal to the deceleration for this frame
            }
            else
            {
                velocity -= velocity.normalized * decelerationThisFrame; // Apply deceleration in the opposite direction of the current velocity
            }

        }

        transform.position += velocity * Time.deltaTime;
    }

    public void Defeat()
    {
        if (isDefeated)
        {
            return;
        }

        isDefeated = true;
        velocity = Vector3.zero;
        GetComponentInChildren<SpriteRenderer>().enabled = false; // Hide the enemy sprite
        StartCoroutine(Respawn());
    }

    public bool IsDefeated()
    {
        return isDefeated;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = initialPosition; // Reset the enemy's position to the initial position

        yield return StartCoroutine(BlinkEffect()); // Start the blink effect before respawning
         
        GetComponentInChildren<SpriteRenderer>().enabled = true; // Show the enemy sprite again
        isDefeated = false;
    }

    private IEnumerator BlinkEffect()
    {
        float blinkTime = 0f;

        while (blinkTime < blinkDuration)
        {
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(blinkInterval);
            blinkTime += blinkInterval;
        }
    }
}
