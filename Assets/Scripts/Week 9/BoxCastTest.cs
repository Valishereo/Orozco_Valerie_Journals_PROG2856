using UnityEngine;

public class BoxCastTest : MonoBehaviour
{
    public float boxCastSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(boxCastSize, boxCastSize), 0, Vector2.right, 12f);

        Debug.DrawRay(transform.position, Vector2.right * 12f, Color.yellow);

        if (hit.collider != null)
        {
            Debug.Log("Detected: " + hit.collider.name);
        }

    }

    
}
