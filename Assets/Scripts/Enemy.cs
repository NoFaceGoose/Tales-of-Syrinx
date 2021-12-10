using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyFire;
    public Transform Left;
    public Transform Right;
    public bool shoot;
    public float WalkSpeed;
    public float StayTime;
    public bool OnPatrol;
    public bool TowardsLeft;
    private bool _onMove = true;
    private bool _stay = false;
    public int CollisionDamage = 1;

    // Add health
    public int health = 1;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (shoot)
        {
            InvokeRepeating("Fire", 0.5f, 1.0f);
        }
    }

    void FixedUpdate()
    {
        if (OnPatrol && _onMove)
        {
            _stay = false;
            if (TowardsLeft == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * WalkSpeed);
            }
        }
    }

    void Update()
    {
        if (OnPatrol && !_stay)
        {
            if (transform.position.x <= Left.position.x)
            {
                _onMove = false;
                TowardsLeft = false;
                Invoke("Move", StayTime);
                _stay = true;
            }
            if (transform.position.x >= Right.position.x)
            {
                _onMove = false;
                TowardsLeft = true;
                Invoke("Move", StayTime);
                _stay = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(CollisionDamage);
        }
    }

    void Fire()
    {
        Instantiate(EnemyBullet, EnemyFire.position, EnemyFire.rotation);
    }

    void Move()
    {
        _onMove = true;
        // Debug.Log(_onMove);
    }
}
