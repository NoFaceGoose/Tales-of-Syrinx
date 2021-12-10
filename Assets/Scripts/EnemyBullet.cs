using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public int EnemyBulletDamage = 1;
    public Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = transform.right * Speed;
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(EnemyBulletDamage);
        }
        if (!hitInfo.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}