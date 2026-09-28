using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float detectionRange; // Range within which the enemy can detect the player
    public float moveSpeed;

    private void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < detectionRange)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction = direction.normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            Debug.Log("Player has been detected");
        }

    }

}
