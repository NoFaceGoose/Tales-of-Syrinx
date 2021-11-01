using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    public Rigidbody Bullet;
    public Transform Fire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other)
    {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(Bullet,Fire.position,Fire.rotation);
            clone.velocity = transform.TransformDirection(Vector3.forward*50);
    }
}