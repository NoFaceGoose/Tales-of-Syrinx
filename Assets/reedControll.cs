using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reedControll : MonoBehaviour
{
    // how long will the reed platform exist
    public float lifetime = 5.0f;
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

    // Stop the reed platform
    void reedStop()
    {
        Debug.Log("Stop!");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        moveFlag = false;
    }
    
    void FixedUpdate()
    {
        // check if the reed has traveled the distance
        if(moveFlag && rb.position.x - rawPos.x >= allowedDifference)
        {
            reedStop();
        }

        // check if the reed has collided and stopped
        if(moveFlag && rb.velocity.x < 0.1f)
        {
            reedStop();
        }
    }
}
