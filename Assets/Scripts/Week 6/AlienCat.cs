using UnityEngine;
using System.Collections.Generic;

public class Alien : MonoBehaviour
{
    public List<Transform> planetTransforms;
    public float movementSpeed = 2f;
    public float maxRotationSpeed = 360f;
    public float minRotationSpeed = 30f;
    private int currentPlanetIndex = 0;
    private float initialDistance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if(planetTransforms.Count > 0)
        {
            initialDistance = Vector3.Distance(transform.position, planetTransforms[currentPlanetIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Transform targetPlanet = planetTransforms[currentPlanetIndex];
        Vector3 direction = targetPlanet.position - transform.position;
        direction = direction.normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetPlanet.position);
        float progress = 1f - (distance / initialDistance);
        progress = Mathf.Clamp01(progress); // Ensure progress is between 0 and 1

        float rotationSpeed = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, progress);
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        if (distance <= 0.2f)
        {
            Debug.Log("Alien reached planet!");
            currentPlanetIndex++;

            if (currentPlanetIndex >= planetTransforms.Count)
            {
                currentPlanetIndex = 0; // Reset to the first planet after reaching the last one
            }

            initialDistance = Vector3.Distance(transform.position, planetTransforms[currentPlanetIndex].position);
        }
    }
}
