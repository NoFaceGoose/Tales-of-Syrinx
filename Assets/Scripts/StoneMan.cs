using UnityEngine;

public class StoneMan : MonoBehaviour
{
    public GameObject Stone;
    public Transform ThrowPoint;
    public Animator Anim;
    public Rigidbody _rigidBody;

    public int reward = 1; // Reward of killing the enemy

    // Enemy patrol
    public float WalkSpeed;
    public float StayTime;
    public bool OnPatrol;
    public bool TowardsLeft;
    public Transform Left;
    public Transform Right;
    private bool _startToAttack = false;
    private bool _onMove = true;
    private bool _readyToStay = true;

    // Enemy health
    public int Health = 3;
    private bool _alive = true;

    public int CollisionDamage = 1; // Enemy collision damage

    public float detectionDistance = 5.0f; // Enemy player detection distance

    // Enemy state
    private enum EnemyAnimState
    {
        stay,
        walk,
        attack,
        attacked,
        die
    }

    private EnemyAnimState _state = EnemyAnimState.walk; // Default state

    // Three lines to detect player
    private bool PlayerCheck()
    {
        if (!OnPatrol)
        {
            return true;
        }

        bool hitPlayer = false;

        RaycastHit hitCenter;
        RaycastHit hitTop;
        RaycastHit hitBottom;

        Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y, _rigidBody.position.z), Color.red);
        Debug.DrawLine(new Vector3(_rigidBody.position.x, _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), Color.red);
        Debug.DrawLine(new Vector3(_rigidBody.position.x, _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), new Vector3(_rigidBody.position.x + (TowardsLeft ? -detectionDistance : detectionDistance), _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), Color.red);

        if (Physics.Raycast(_rigidBody.position, TowardsLeft ? Vector3.left : Vector3.right, out hitCenter, detectionDistance))
        {
            if (hitCenter.collider.CompareTag("Player") || hitCenter.collider.CompareTag("Bullet") || hitCenter.collider.CompareTag("ReedPlatform"))
            {
                hitPlayer = true;
            }
        }

        if (Physics.Raycast(new Vector3(_rigidBody.position.x, _rigidBody.position.y + transform.localScale.y / 4, _rigidBody.position.z), TowardsLeft ? Vector3.left : Vector3.right, out hitTop, detectionDistance))
        {
            if (hitTop.collider.CompareTag("Player") || hitTop.collider.CompareTag("Bullet") || hitTop.collider.CompareTag("ReedPlatform"))
            {
                hitPlayer = true;
            }
        }

        if (Physics.Raycast(new Vector3(_rigidBody.position.x, _rigidBody.position.y - transform.localScale.y / 4, _rigidBody.position.z), TowardsLeft ? Vector3.left : Vector3.right, out hitBottom, detectionDistance))
        {
            if (hitBottom.collider.CompareTag("Player") || hitBottom.collider.CompareTag("Bullet") || hitBottom.collider.CompareTag("ReedPlatform"))
            {
                hitPlayer = true;
            }
        }

        return hitPlayer;
    }

    // What happens after enemy is hit
    public void TakeDamage(int damage)
    {
        if (Health % 3 == 0)
        {
            _state = EnemyAnimState.attacked;
        }
        Health -= damage;

        // turn to player
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
        _alive = false;
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

    void Start()
    {
        if (!OnPatrol)
        {
            _state = EnemyAnimState.stay;
        }
    }

    void FixedUpdate()
    {
        if (_alive)
        {
            if (OnPatrol && !_startToAttack)
            {
                // Patrol
                if (_onMove)
                {
                    _readyToStay = true;
                    _state = EnemyAnimState.walk;
                    transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
                }

                // Stay
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

            // Detect Player
            if (PlayerCheck())
            {
                if (IsInvoking("Move"))
                {
                    CancelInvoke("Move");
                }

                _onMove = false;
                _startToAttack = true;
                _state = EnemyAnimState.attack;

                if (IsInvoking("StopAttacking"))
                {
                    CancelInvoke("StopAttacking");
                }
            }
            else
            {
                if (_state == EnemyAnimState.attack)
                {
                    Invoke("StopAttacking", StayTime * 2);
                }
            }
        }
    }

    void Update()
    {
        // Enemy state switch
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
        // Damage player
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.GetPlayerStatus())
            {
                player.TakeDamage(CollisionDamage);
            }
        }

        // Destroyed by reed platform
        if (other.gameObject.CompareTag("ReedPlatform"))
        {
            Destroy(other.gameObject);
        }
    }

    void Attack()
    {
        Instantiate(Stone, ThrowPoint.position, ThrowPoint.rotation);
    }

    // Move after staying

    void Move()
    {
        if (!_readyToStay)
        {
            TowardsLeft = !TowardsLeft;
            transform.Rotate(0f, 180f, 0f);
        }
        _onMove = true;
    }

    void StopAttacking()
    {
        _startToAttack = false;
        _state = EnemyAnimState.stay;
        Invoke("Move", StayTime);
    }
}