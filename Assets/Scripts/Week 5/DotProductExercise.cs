using UnityEngine;
using UnityEngine.InputSystem;

public class DotProductExercise : MonoBehaviour
{

    public float redAngle;
    public float blueAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 redVector = ComputeVectorFromAngle(redAngle);
        Vector3 blueVector = ComputeVectorFromAngle(blueAngle);

        Debug.DrawLine(Vector3.zero, redVector, Color.red);
        Debug.DrawLine(Vector3.zero, blueVector, Color.blue);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        { 
            float dot = ComputeDotProduct(redVector, blueVector);
            Debug.Log($"Dot Product: {dot}"); 

        }
    }

    private Vector3 ComputeVectorFromAngle(float angle)
    {
        float angleRadians = angle * Mathf.Deg2Rad; // Convert angle to radians

        float x = Mathf.Cos(angleRadians);
        float y = Mathf.Sin(angleRadians);
        

        return new Vector3(x, y);
    }

    private float ComputeDotProduct(Vector3 a, Vector3 b)
    {
       float dot = a.x * b.x + a.y * b.y + a.z * b.z;
        return dot;
    }
}
