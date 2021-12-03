using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reed_control : MonoBehaviour
{
    // how long will the reed platform exist
    public float lifetime = 1.0f;
    public float allowedDifference = 5.0f; // how far will the reed platform travel
    public float speed = 20f; // reed platform speed
    public Rigidbody rb;
    private Vector3 rawPos; // stor raw position
    private bool moveFlag = false; // if the reed platform is moving
    //private GameObject obj;
    void Awake()
    {
        Destroy(gameObject, lifetime);
        rawPos = rb.position;
        rb.velocity = new Vector3(speed, 0.0f, 0.0f);
        moveFlag = true;
    }
    
    void FixedUpdate()
    {
        if(moveFlag && rb.position.x - rawPos.x >= allowedDifference)
        {
            Debug.Log("on dis!");
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            moveFlag = false;
        }
    }
}
