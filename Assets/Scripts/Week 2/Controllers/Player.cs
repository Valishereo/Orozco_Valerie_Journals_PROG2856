using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    void Start()
    {
        Debug.Log(NormalizeVector(new Vector3(3, 4)));
        Debug.Log(NormalizeVector(new Vector3(-3, 4)));
        Debug.Log(NormalizeVector(new Vector3(1.5f, -3.5f)));
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
            {
            SpawnBombAtOffSet(Vector3.up);
            }
    }
    void SpawnBombAtOffSet(Vector3 inOffset)
    {
        Vector3 playerPos = transform.position;
        Vector3 spawnPos = playerPos + inOffset;

        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }

    Vector2 NormalizeVector(Vector2 inVector)
    {
        float magnitude = inVector.magnitude;
        Vector2 outVector = new Vector2(inVector.x / magnitude, inVector.y / magnitude);
        return outVector;
    }
}
