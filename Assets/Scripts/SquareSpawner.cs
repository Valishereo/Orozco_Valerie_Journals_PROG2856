using UnityEngine;
using UnityEngine.InputSystem;

public class SquareSpawner : MonoBehaviour
{
    public float squareSize = 2f;
    Vector2 clickedPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, 10f);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //Vector2 endPosition = new Vector2(worldPosition.x + 1f, worldPosition.y);

        Vector2 topLeft = new Vector2(worldPosition.x - squareSize / 2, worldPosition.y + squareSize / 2);
        Vector2 topRight = new Vector2(worldPosition.x + squareSize / 2, worldPosition.y + squareSize / 2);
        Vector2 bottomLeft = new Vector2(worldPosition.x - squareSize / 2, worldPosition.y - squareSize / 2);
        Vector2 bottomRight = new Vector2(worldPosition.x + squareSize / 2, worldPosition.y - squareSize / 2);

        //Debug.DrawLine(worldPosition, endPosition, Color.white);

        Color sTWhite = new Color(1f, 1f, 1f, 0.3f);

        Debug.DrawLine(topLeft, topRight, sTWhite);
        Debug.DrawLine(topRight, bottomRight, sTWhite);
        Debug.DrawLine(bottomRight, bottomLeft, sTWhite);
        Debug.DrawLine(bottomLeft, topLeft, sTWhite);
    }
}
