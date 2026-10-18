using UnityEngine;
using System.Collections.Generic;

public class Alien : MonoBehaviour
{
    public List<Transform> planetTransforms;
    public float movementSpeed = 2f;
    private int currentPlanetIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform targetPlanet = planetTransforms[currentPlanetIndex];
        Vector3 direction = targetPlanet.position - transform.position;
        direction = direction.normalized;

        transform.position += direction * movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetPlanet.position);

        if (distance <= 0.2f)
        {
            Debug.Log("Alien reached planet!");
            currentPlanetIndex++;

            if (currentPlanetIndex >= planetTransforms.Count)
            {
                currentPlanetIndex = 0; // Reset to the first planet after reaching the last one
            }
        }
    }
}
