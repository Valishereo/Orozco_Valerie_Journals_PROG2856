using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    Vector2 previousPosition;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); // Get the current mouse position in screen coordinates
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, 10f);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            previousPosition = worldPosition;
            timer = 0f;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            timer += Time.deltaTime;

            if (timer >= 0.1f) // Adjust the threshold as needed
            {
                Debug.DrawLine(previousPosition, worldPosition, Color.white, 100f);
                previousPosition = worldPosition;
                timer = 0f;
            }
        }
    }
}
