using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    Vector2 previousPosition;
    float timer = 0f;
    float totalLength = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // To get the current mouse position in screen coordinates
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, 10f);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            previousPosition = worldPosition;
            timer = 0f;
            totalLength = 0f;
        }

        if (Mouse.current.leftButton.isPressed) 
        {
            timer += Time.deltaTime;

            if (timer >= 0.1f) // This checks if 0.1 seconds have passed since the last segment was drawn
            {
                Debug.DrawLine(previousPosition, worldPosition, Color.white, 100f); // For drawing a line segment between the previous and current mouse positions

                Vector2 diference = worldPosition - previousPosition;
                // This calculates the length of the segment using the distance formula from class
                float segmentLength = Mathf.Sqrt(diference.x * diference.x + diference.y * diference.y); 

                totalLength += segmentLength;

                previousPosition = worldPosition;
                timer = 0f;
            }
            
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame) 
        {
            // Total length of the pipeline drawn when the mouse button is released

            Debug.Log("Total pipeline length: " + totalLength);
        }
    }
}
