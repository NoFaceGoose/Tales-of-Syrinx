using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Rigidbody rb;
    public float tumbleSpeed;
    public bool keyB=false;
    private Vector3 offset;
    private Transform playerTrans;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
        playerTrans = GameObject.FindWithTag("Player").transform;
        offset = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(keyB==false)
        {
            if (rb.angularVelocity.z < 0.5)
            {
                rb.angularVelocity = Random.insideUnitSphere * tumbleSpeed;
            }
        }
        else
        {
            transform.position = playerTrans.position + offset;
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
        {
      keyB=true;
      }
    }
}
