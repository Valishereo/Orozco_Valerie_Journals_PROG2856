using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float movementSpeed = 1.4f;
    private bool isLaunched = false;
    public float launchDelay = 2.5f; // Delay before the rocket launches
    private float flightTime = 0f;
    public float rotationSpeed = 90f;
    public float explosionDistance = 0.5f;

    public Transform enemyTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {

            transform.position += transform.up * movementSpeed * Time.deltaTime;

            flightTime += Time.deltaTime;

            if (flightTime >= launchDelay)
            {
                Vector3 direction = enemyTransform.position - transform.position;
                direction = direction.normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90f;

                float currentAngle = transform.eulerAngles.z;
                float newAngle = Mathf.MoveTowardsAngle(currentAngle, angle, rotationSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

                //Debug.Log("Homing activated!");
            }

            float distance = Vector3.Distance(transform.position, enemyTransform.position);

            if (distance <= explosionDistance)
            {
                Debug.Log("Enemy hit!");
                Destroy(gameObject);
            }

        }
    }

    public void Launch()
    {
        isLaunched = true;
    }   
}
