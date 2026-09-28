using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;

    private Vector3 currentPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 previousPosition;
    private int currentStar = 0;
    private float currentDrawingTime = 0f;

    void Start()
    {
        previousPosition = starTransforms[0].position; // Initialize previousPosition to the position of the first star
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    private void DrawConstellation()
    {
        startPosition = starTransforms[currentStar].position;
        endPosition = starTransforms[currentStar + 1].position;

        currentDrawingTime += Time.deltaTime;

        float ratio = currentDrawingTime / drawingTime; 
        currentPosition = Vector3.Lerp(startPosition, endPosition, ratio);
        Vector3 erasePosition = Vector3.Lerp(previousPosition, startPosition, ratio);

        // Draw the line and fading line for the constellation

        Debug.DrawLine(erasePosition,startPosition, Color.white); 
        Debug.DrawLine(startPosition, currentPosition, Color.white);

        if (currentDrawingTime >= drawingTime) // Check if the drawing time for the current star is complete
        {
            currentDrawingTime = 0f;
            previousPosition = startPosition;
            currentStar++;

            if (currentStar >= starTransforms.Count - 1)
            {
                currentStar = 0; // Reset to the first star after reaching the last one
            }
        }
    }
}
