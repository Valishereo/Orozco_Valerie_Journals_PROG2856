using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SquareSpawner : MonoBehaviour
{
    public float squareSize = 2f;
    //Vector2 clickedPosition;
    //bool hasClicked = false;

    List<Vector2> clickedPositions = new List<Vector2>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Tracking mouse position in world space

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, 10f);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //Vector2 endPosition = new Vector2(worldPosition.x + 1f, worldPosition.y);

        if (Mouse.current.leftButton.wasPressedThisFrame) //To draw white square when mouse is clicked
        {
            clickedPositions.Add(worldPosition);
            //hasClicked = true;
        }

        //White square drawing in clicked position

        for (int i = 0; i < clickedPositions.Count; i++)
        {
            Vector2 currentPos = clickedPositions[i];

            Vector2 clickedtopLeft = new Vector2(currentPos.x - squareSize / 2, currentPos.y + squareSize / 2);
            Vector2 clickedtopRight = new Vector2(currentPos.x + squareSize / 2, currentPos.y + squareSize / 2);
            Vector2 clickedbottomLeft = new Vector2(currentPos.x - squareSize / 2, currentPos.y - squareSize / 2);
            Vector2 clickedbottomRight = new Vector2(currentPos.x + squareSize / 2, currentPos.y - squareSize / 2);

            Debug.DrawLine(clickedtopLeft, clickedtopRight, Color.white);
            Debug.DrawLine(clickedtopRight, clickedbottomRight, Color.white);
            Debug.DrawLine(clickedbottomRight, clickedbottomLeft, Color.white);
            Debug.DrawLine(clickedbottomLeft, clickedtopLeft, Color.white);
        }

        //Square drawing semiTransparnet

        Vector2 topLeft = new Vector2(worldPosition.x - squareSize / 2, worldPosition.y + squareSize / 2);
        Vector2 topRight = new Vector2(worldPosition.x + squareSize / 2, worldPosition.y + squareSize / 2);
        Vector2 bottomLeft = new Vector2(worldPosition.x - squareSize / 2, worldPosition.y - squareSize / 2);
        Vector2 bottomRight = new Vector2(worldPosition.x + squareSize / 2, worldPosition.y - squareSize / 2);

        //Debug.DrawLine(worldPosition, endPosition, Color.white);

        Color sTWhite = new Color(1f, 1f, 1f, 0.3f); //Color white with 30% opacity

        Debug.DrawLine(topLeft, topRight, sTWhite);
        Debug.DrawLine(topRight, bottomRight, sTWhite);
        Debug.DrawLine(bottomRight, bottomLeft, sTWhite);
        Debug.DrawLine(bottomLeft, topLeft, sTWhite);
    }
}
