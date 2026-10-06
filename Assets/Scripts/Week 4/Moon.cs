using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float radius = 2;
    public float speed = 10;

    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radius, speed, planetTransform);
    }

    public void OrbitalMotion (float radius, float speed, Transform target)
    {
        angle += speed * Time.deltaTime; // Update the angle based on speed and time

        float angleRadians = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(angleRadians) * radius;
        float y = Mathf.Sin(angleRadians) * radius;

        Vector3 moonPosition = target.position + new Vector3(x, y, 0); // Calculate the moon's position relative to the planet
        transform.position = moonPosition; 
    }
}
