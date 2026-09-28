using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange; // Range within which the enemy can detect the player
    public float moveSpeed;
    public float decelerationTime = 5f; // Time it takes for the enemy to decelerate to a stop

    private Vector3 velocity;

    private void Update()
    {
        EnemyMovement();
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

}
