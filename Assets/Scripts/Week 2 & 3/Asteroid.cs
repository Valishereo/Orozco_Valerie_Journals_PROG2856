using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        SetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement(); 
    }

    public void AsteroidMovement()
    {
        
        float distance = Vector3.Distance(transform.position, targetPosition); // Calculate the distance between the asteroid's current position and the target position

        if (distance <= arrivalDistance) 
        { 
            SetNewTarget();

        }
        
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;

        transform.position += direction * moveSpeed * Time.deltaTime; 
    }

    private void SetNewTarget()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f); // Generate a random direction
        randomDirection = randomDirection.normalized;
        targetPosition = transform.position + randomDirection * maxFloatDistance; // Set the new target position based on the random direction and max float distance

    }
}
