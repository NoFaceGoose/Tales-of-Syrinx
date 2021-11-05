using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Application.LoadLevel("Lose");
        }
    }
    
    public void Fire()
    {
        GameObject shootingPoint = GameObject.Find("EnemyFire");
        Instantiate(EnemyBullet, shootingPoint.GetComponent<Transform>().position, shootingPoint.GetComponent<Transform>().rotation);
    }
}
