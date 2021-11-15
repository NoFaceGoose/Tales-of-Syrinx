using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = transform.right * Speed;
        // Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 50);
    //     transform.position = transform.position + new Vector3(Speed * Time.deltaTime, 0f, 0f);
    // }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Destroy(hitInfo.gameObject);
            Destroy(gameObject);
            return;
        }
        if (hitInfo.CompareTag("Boss"))
        {
            Boss.BossInstance.Improve();
            return;
        }
        if (!hitInfo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}