using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float movementSpeed = 1.4f;
    private bool isLaunched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaunched)
        {
            transform.position += transform.up * movementSpeed * Time.deltaTime;
        }
    }

    public void Launch()
    {
        isLaunched = true;
    }   
}
