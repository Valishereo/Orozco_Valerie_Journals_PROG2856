using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("Measured in degrees")]
    public float angularSpeed = 60f;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, angularSpeed * Time.deltaTime);

        Debug.DrawLine(transform.position, transform.position + transform.up, Color.white);

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        //float dot = Vector3.Dot(transform.up, directionToTarget);

        //if (dot >= 0)
        //{
        //    Debug.Log("In front");
        //}
        //else
        //{
        //    Debug.Log("Behind");

        //}

        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle (upAngle, directionAngle);

        //Debug.Log(deltaAngle);

        float dot = Vector3.Dot(transform.up, directionToTarget); 


        if (dot < 0.95f)
        switch(Mathf.Sign(deltaAngle))
        {
            case 1:
                transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
                break;
            case -1: transform.Rotate(0, 0, -angularSpeed * Time.deltaTime);
                break;

        }
    }
}
