using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class UnitCircleExercise : MonoBehaviour
{
    public List<float> angles;
    private int currentAngleIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentAngleIndex++;
            if (currentAngleIndex >= angles.Count)
            {
                currentAngleIndex = 0;
            }
        }

        float angle = angles[currentAngleIndex]; // Get the current angle from the list

        float angleRadians = angle * Mathf.Deg2Rad; // Convert angle to radians

       // Calculate the x and y coordinates on the unit circle
        float x = Mathf.Cos(angleRadians);
        float y = Mathf.Sin(angleRadians);

        Vector3 point = new Vector3(x, y, 0);

        Debug.DrawLine(Vector3.zero, point, Color.white);  // Draw a line from the origin to the point on the unit circle
    }
}
