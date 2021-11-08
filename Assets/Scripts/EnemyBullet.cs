using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}