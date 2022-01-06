using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public static PlayerController PlayerInstance;

    public Vector3 SavedPosition;

    public float MoveSpeed;
    public float jumpForce = 5.0f;
    // public float Gravity;

    private int _keys = 0;

    // jump function
    public LayerMask GroundLayerMask; // only check ground level
    private Vector3 _jump;
    public int jumpCount = 1; //Make the player able to double jump
    private Rigidbody _rigidBody;
    private float _inputX;
    // test the height
    // public float extraHeightText = 1;

    // Flip
    private bool _isFacingRight = true;

    // Fire
    public Transform FirePoint;
    public GameObject BulletPrefab;

    // Launch the reed
    public GameObject ReedPrefab;
    public int ReedMaxCount = 1;
    public int ReedCount;
    public float ReedRate = 1.0f;
    private float nextReed = 1.0f;

    // Health
    public int MaxHealth = 4;
    public int CurrentHealth;
    public HealthBar healthBar;

    // set the player Invincible
    private bool playerInvincible = false;
    public float InvincibleTime = 2.0f;

    // Animation
    public Animator animator;

    public DieMenu dm;

    void Awake()
    {
        PlayerInstance = this;
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        SavedPosition = transform.position;
        ReedCount = ReedMaxCount;
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    void Update()
    {
        if (InvincibleTime > 0 && playerInvincible)
        {
            InvincibleTime -= Time.deltaTime;
        }
        else
        {
            playerInvincible = false;
            InvincibleTime = 2.0f;
        }
        if (Time.time > nextReed)
        {
            nextReed = Time.time + ReedRate;
            ReedCount = ReedMaxCount;
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector3(_inputX * MoveSpeed, _rigidBody.velocity.y, 0);
        animator.SetFloat("Speed", _rigidBody.velocity.x);
        // Physics.gravity = new Vector3(0, Gravity, 0);

        // if is on ground, can double jump
        if (GroundCheck())
        {
            jumpCount = 1;
            animator.SetBool("inAir", false);
        }
        else
        {
            animator.SetBool("inAir", true);
        }
        // _isGrounded = Physics.Raycast(transform.position, Vector3.down, _disToGround);
        if (CurrentHealth < 1)
        {
            animator.SetTrigger("Death");
            Die();
        }
    }

    public void Die()
    {
        dm.EnterDieMenu();
        Reborn(); // in reborn, the state of the animation should be reset\\
    }

    // return the value that if the player is invincible
    public bool GetPlayerStatus()
    {
        return playerInvincible;
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Attacked");
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);
        playerInvincible = true;
    }

    public void Recover(int reward)
    {
        CurrentHealth += reward;
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        healthBar.SetHealth(CurrentHealth);
    }

    private bool GroundCheck()
    {
        float extraHeightText = 0.88f;
        bool raycastHit = Physics.Raycast(_rigidBody.position, Vector3.down, extraHeightText, GroundLayerMask);
        Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x, _rigidBody.position.y - extraHeightText, _rigidBody.position.z), Color.red);
        return raycastHit;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _inputX = value.ReadValue<Vector2>().x;

        // Flip the character
        if (_inputX > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_inputX < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed && jumpCount > 0)
        {
            Jump();
            jumpCount -= 1;
        }
    }

    private void Jump()
    {
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, jumpForce, 0);
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            animator.SetTrigger("Attack");
            Shoot();
        }
    }

    private void Shoot()
    {
        // shooting logic
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    // Launch the reed platform
    public void LaunchReed(InputAction.CallbackContext value)
    {
        if (value.performed && ReedCount > 0)
        {
            animator.SetTrigger("Reed");
            Instantiate(ReedPrefab, FirePoint.position, FirePoint.rotation);
            ReedCount -= 1;
        }
    }

    public void UpdateReedCount(int newCount)
    {
        ReedMaxCount = newCount;
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public int SetKeys()
    {
        return _keys++;
    }

    public int GetKeys()
    {
        return _keys;
    }

    public void Reborn()
    {
        transform.position = SavedPosition;
        CurrentHealth = MaxHealth;
        healthBar.SetHealth(CurrentHealth);
    }
}