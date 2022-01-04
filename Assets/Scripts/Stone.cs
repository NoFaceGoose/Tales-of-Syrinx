using UnityEngine;

public class Stone : MonoBehaviour
{
    public float Speed = -20f;
    public int Damage = 2;
    public int Health = 3;
    public Rigidbody _rigidbody;
    public Sprite StoneSprite;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = transform.right * Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Health < 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = StoneSprite;
            Damage--;
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayerController player = hitInfo.GetComponent<PlayerController>();
            if (player != null && !player.GetPlayerStatus())
            {
                player.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }

        if (hitInfo.CompareTag("Bullet"))
        {
            Debug.Log("1");
            Health--;
            Destroy(hitInfo.gameObject);
            if (Health <= 0)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (!hitInfo.CompareTag("Enemy") && !hitInfo.CompareTag("EnemyMissile") && !hitInfo.CompareTag("CheckPoint"))
        {
            Destroy(gameObject);
        }
    }
}