using UnityEngine;

public class ClosestPointTest : MonoBehaviour
{
    public Transform caster;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 closestPoint = rb.ClosestPoint(caster.position);

        Debug.DrawLine (caster.position, closestPoint, Color.green);
    }
}
