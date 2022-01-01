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
    private bool _readyToStay = true;
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
    public float FireInterval = 1.5f; // the interval between 2 fire

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
        bool hitPlayer = false;

        RaycastHit hitCenter;
        RaycastHit hitTop;
        RaycastHit hitBottom;

        Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y, _rigidBody.position.z), Color.red);
        Debug.DrawLine(new Vector3(_rigidBody.position.x, _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), Color.red);
        Debug.DrawLine(new Vector3(_rigidBody.position.x, _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), Color.red);

        if (Physics.Raycast(_rigidBody.position, TowardsLeft ? Vector3.left : Vector3.right, out hitCenter, detectionDistance))
        {
            if (hitCenter.collider.CompareTag("Player"))
            {
                hitPlayer = true;
            }
        }

        if (Physics.Raycast(new Vector3(_rigidBody.position.x, _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), TowardsLeft ? Vector3.left : Vector3.right, out hitTop, detectionDistance))
        {
            if (hitTop.collider.CompareTag("Player"))
            {
                hitPlayer = true;
            }
        }

        if (Physics.Raycast(new Vector3(_rigidBody.position.x, _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), TowardsLeft ? Vector3.left : Vector3.right, out hitBottom, detectionDistance))
        {
            if (hitBottom.collider.CompareTag("Player"))
            {
                hitPlayer = true;
            }
        }

        return hitPlayer;
    }

    public void TakeDamage(int damage)
    {
        _state = EnemyAnimState.attacked;
        Health -= damage;
        if (!PlayerCheck())
        {
            TowardsLeft = !TowardsLeft;
            transform.Rotate(0f, 180f, 0f);
        }
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _state = EnemyAnimState.die;
        _onMove = false;
        _readyToStay = false;
        _startShoot = false;
        PlayerController.PlayerInstance.Recover(reward);
        Invoke("Destroy", 1f);
    }

    void Destroy()
    {
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
        if (OnPatrol && !_startShoot)
        {

            if (_onMove)
            {
                _readyToStay = true;
                _state = EnemyAnimState.walk;
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }

            if (_readyToStay)
            {
                if ((transform.position.x <= Left.position.x && TowardsLeft) || (transform.position.x >= Right.position.x && !TowardsLeft))
                {
                    _onMove = false;
                    Invoke("Move", StayTime);
                    _readyToStay = false;
                    _state = EnemyAnimState.stay;
                }
            }
        }

        if (PlayerCheck())
        {
            _onMove = false;
            _startShoot = true;
            CancelInvoke("Cancel");
            _state = EnemyAnimState.attack;
            InvokeRepeating("Fire", FireInterval, FireInterval);
        }
        else
        {
            if (_startShoot)
            {
                _startShoot = false;
                Invoke("Cancel", FireInterval * 3);
            }
        }
    }

    void Update()
    {
        switch (_state)
        {
            case EnemyAnimState.stay:
                Anim.SetBool("IsWalking", false);
                Anim.SetBool("IsAttacking", false);
                Anim.SetBool("IsAttacked", false);
                break;

            case EnemyAnimState.walk:
                Anim.SetBool("IsWalking", true);
                break;

            case EnemyAnimState.attack:
                Anim.SetBool("IsWalking", false);
                Anim.SetBool("IsAttacked", false);
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

    void KeepMoving()
    {
        _onMove = true;
    }

    void Cancel()
    {
        CancelInvoke("Fire");
        _state = EnemyAnimState.stay;
        Invoke("KeepMoving", StayTime);
    }
}
