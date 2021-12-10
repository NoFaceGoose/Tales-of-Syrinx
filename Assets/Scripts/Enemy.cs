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
    public float DetectDistance = 1f;
    private bool _onMove = true;
    private bool _stay = false;
    public int CollisionDamage = 1;

    // Enemy health
    public int health = 1;


    // Enemy player detection
    public float detectionDistance = 5.0f; // detect distance
    public Rigidbody _rigidBody;
    public LayerMask PlayerLayerMask; // detect player
    public float FireInterval = 0.8f; // the interval between 2 fire
    private float LastFire;

    private bool PlayerCheck()
    {
        bool raycastHit = Physics.Raycast(_rigidBody.position, Vector3.left, 0, PlayerLayerMask);
        if(TowardsLeft)
        {
            raycastHit = Physics.Raycast(_rigidBody.position, Vector3.left, detectionDistance, PlayerLayerMask);
            Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x-detectionDistance, _rigidBody.position.y, _rigidBody.position.z), Color.red);
        }
        else
        {
            raycastHit = Physics.Raycast(_rigidBody.position, Vector3.right, detectionDistance, PlayerLayerMask);
            Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x+detectionDistance, _rigidBody.position.y, _rigidBody.position.z), Color.red);
        }
        return raycastHit;
    }

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
        // InvokeRepeating("Fire", 0.5f, 1.0f);
    }

    // Start is called before the first frame update
    void Start()
    {

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
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }
        }

        if (OnPatrol && !_stay)
        {
            if (transform.position.x <= Left.position.x && TowardsLeft)
            {
                _onMove = false;
                TowardsLeft = false;
                Invoke("Move", StayTime);
                _stay = true;
            }
            if (transform.position.x >= Right.position.x && !TowardsLeft)
            {
                _onMove = false;
                TowardsLeft = true;
                Invoke("Move", StayTime);
                _stay = true;
            }
        }

        // Check if the player in the distance and can fire
        if (PlayerCheck() && LastFire > 0.01f)
        {
            if(LastFire > FireInterval)
            {
                Fire();
            }
            LastFire -= Time.deltaTime;
            
        } else {
            LastFire = FireInterval + 0.01f;
        }
    }

    void Update()
    {
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
        transform.Rotate(0f, 180f, 0f);
        _onMove = true;
    }
}
