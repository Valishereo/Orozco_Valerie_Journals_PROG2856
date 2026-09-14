using UnityEngine;
using TMPro;

public class RowGeneration : MonoBehaviour
{
    public TMP_InputField squareNumberInput;
    float squareSize = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRow()
    {
        int squareNumber;

        if (int.TryParse(squareNumberInput.text, out squareNumber) && squareNumber > 0) // Check if the input is a valid integer
        {
           Debug.Log ("Valid number:" + squareNumber);

            for (int i = 0; i < squareNumber; i++) // Loop through the number of squares to generate
            {
                Vector2 squarePosition = new Vector2(i * squareSize, 0f);
                //Debug.Log("Square " + i);


                // Draw the square using Debug.DrawLine

                Vector2 topLeft = new Vector2(squarePosition.x - squareSize / 2, squarePosition.y + squareSize / 2);
                Vector2 topRight = new Vector2(squarePosition.x + squareSize / 2, squarePosition.y + squareSize / 2);
                Vector2 bottomLeft = new Vector2(squarePosition.x - squareSize / 2, squarePosition.y - squareSize / 2);
                Vector2 bottomRight = new Vector2(squarePosition.x + squareSize / 2, squarePosition.y - squareSize / 2);

                Debug.DrawLine(topLeft, topRight, Color.white, 100f);
                Debug.DrawLine(topRight, bottomRight, Color.white, 100f);
                Debug.DrawLine(bottomRight, bottomLeft, Color.white, 100f);
                Debug.DrawLine(bottomLeft, topLeft, Color.white, 100f);
            }
        }

        else
        {
            Debug.Log("Invalid input");
        }


        //Debug.Log("Generate button clicked");
    }
}
