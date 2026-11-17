using UnityEngine;

public class AngularVelocityTest : MonoBehaviour
{
    private Rigidbody2D rb;
    public float angularVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = angularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
