using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 5.0f;
    private float zDestroy = -21f;

    public Rigidbody objectRb;

    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        // basic movement non-player objects
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);
        // Destroy objects off-screen
        if(transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
