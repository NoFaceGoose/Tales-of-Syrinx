using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Rigidbody rb;
    public float tumbleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.angularVelocity.z < 0.5)
        {
            rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
        }
    }
}
