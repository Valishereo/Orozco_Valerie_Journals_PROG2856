using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Threading;

public class UnitCircleExercise : MonoBehaviour
{
    public List<float> angles;
    public float radius =1f;
    public Vector3 circlePosition;
    public float angleDuration = 1f; // Duration to stay at each angle
    private int currentAngleIndex = 0;
    private float angleTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angleTimer += Time.deltaTime;

        if (angleTimer >= angleDuration)
        {
            currentAngleIndex++;
            angleTimer = 0f;

            if (currentAngleIndex >= angles.Count)
            {
                currentAngleIndex = 0;
            }
        }

        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    currentAngleIndex++;
        //    if (currentAngleIndex >= angles.Count)
        //    {
        //        currentAngleIndex = 0;
        //    }
        //}

        float angle = angles[currentAngleIndex]; // Get the current angle from the list

        float angleRadians = angle * Mathf.Deg2Rad; // Convert angle to radians

       // Calculate the x and y coordinates on the unit circle
        float x = Mathf.Cos(angleRadians) * radius;
        float y = Mathf.Sin(angleRadians) * radius;

        Vector3 point = circlePosition + new Vector3(x, y, 0);

        Debug.DrawLine(circlePosition, point, Color.white);  // Draw a line from the origin to the point on the unit circle
    }
}
