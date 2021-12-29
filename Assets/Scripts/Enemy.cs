using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyFire;
    public Animator Anim;

    public bool shoot;

    // reward of killing the enemy
    public int reward = 1;

    // Enemy patrol
    public float WalkSpeed;
    public float StayTime;
    public bool OnPatrol;
    public bool TowardsLeft;
    private bool _startShoot = false;
    private bool _onMove = true;
    private bool _stay = false;
    public Transform Left;
    public Transform Right;

    // Enemy health
    public int Health = 3;

    // Enemy damage
    public int CollisionDamage = 1;

    // Enemy player detection
    public float detectionDistance = 5.0f; // detect distance
    public Rigidbody _rigidBody;
    public LayerMask PlayerLayerMask; // detect player
    public float FireInterval = 5f; // the interval between 2 fire
    private float LastFire;

    private enum EnemyAnimState
    {
        stay,
        walk,
        attack,
        attacked,
        die
    }

    private EnemyAnimState _state = EnemyAnimState.walk;

    private bool PlayerCheck()
    {
        bool raycastHit = Physics.Raycast(_rigidBody.position, Vector3.left, 0, PlayerLayerMask);
        if (TowardsLeft)
        {
            raycastHit = Physics.Raycast(_rigidBody.position, Vector3.left, detectionDistance, PlayerLayerMask);
            Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x - detectionDistance, _rigidBody.position.y, _rigidBody.position.z), Color.red);
        }
        else
        {
            raycastHit = Physics.Raycast(_rigidBody.position, Vector3.right, detectionDistance, PlayerLayerMask);
            Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x + detectionDistance, _rigidBody.position.y, _rigidBody.position.z), Color.red);
        }
        return raycastHit;
    }

    public void TakeDamage(int damage)
    {
        _state = EnemyAnimState.attacked;
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _state = EnemyAnimState.die;
        PlayerController.PlayerInstance.Recover(reward);
        Destroy(gameObject);
    }


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (OnPatrol && !_startShoot && _onMove)
        {
            _stay = false;
            _state = EnemyAnimState.walk;
            if (TowardsLeft == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }
        }

        if (OnPatrol && !_startShoot && !_stay)
        {
            if (transform.position.x <= Left.position.x && TowardsLeft)
            {
                _onMove = false;
                Invoke("Move", StayTime);
                _stay = true;
                _state = EnemyAnimState.stay;
            }
            if (transform.position.x >= Right.position.x && !TowardsLeft)
            {
                _onMove = false;
                Invoke("Move", StayTime);
                _stay = true;
                _state = EnemyAnimState.stay;
            }
        }

        // Check if the player in the distance and can fire
        if (PlayerCheck() && LastFire > 0.01f)
        {
            _startShoot = true;
            _state = EnemyAnimState.stay;
            if (LastFire > FireInterval)
            {
                Fire();
            }
            LastFire -= Time.deltaTime;
        }
        else
        {
            _startShoot = false;
            LastFire = FireInterval + 0.01f;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case EnemyAnimState.stay:
                Anim.SetBool("IsWalking", false);
                Anim.SetBool("IsAttacking", false);
                break;

            case EnemyAnimState.walk:
                Anim.SetBool("IsWalking", true);
                break;

            case EnemyAnimState.attack:
                Anim.SetBool("IsAttacking", true);
                break;

            case EnemyAnimState.attacked:
                Anim.SetBool("IsAttacked", true);
                break;

            case EnemyAnimState.die:
                Anim.SetBool("IsDead", true);
                break;

            default: break;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.GetPlayerStatus())
            {
                player.TakeDamage(CollisionDamage);
            }
        }
        else
        {
            TowardsLeft = !TowardsLeft;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void Fire()
    {
        _state = EnemyAnimState.attack;
        Instantiate(EnemyBullet, EnemyFire.position, EnemyFire.rotation);
    }

    void Move()
    {
        TowardsLeft = !TowardsLeft;
        transform.Rotate(0f, 180f, 0f);
        _onMove = true;
    }
}
