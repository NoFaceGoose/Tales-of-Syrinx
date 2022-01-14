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
    public float extraHeightText = 1;

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
    public float spriteBlinkingDuration = 0.1f;
    public float spriteBlinkingTimer = 0.0f;

    // Animation
    public Animator animator;
    private SpriteRenderer SR;

    public DieMenu dm;

    void Awake()
    {
        PlayerInstance = this;
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        SavedPosition = transform.position;
        ReedCount = ReedMaxCount;
        SR = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    void Update()
    {
        bool isRender = this.gameObject.GetComponent<SpriteRenderer>().enabled;
        if (InvincibleTime > 0 && playerInvincible)
        {
            InvincibleTime -= Time.deltaTime;
            spriteBlinkingTimer += Time.deltaTime;
            if (spriteBlinkingTimer >= spriteBlinkingDuration)
            {
                spriteBlinkingTimer = 0;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = !isRender;

            }
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
        if (CurrentHealth <= 0)
        {
            animator.SetTrigger("Death");
            GetComponent<PlayerInput>().enabled = false;
            // Die();
        }
    }

    public void OnDeathAnimationFinished()
    {
        dm.EnterDieMenu();
        // Reborn(); // in reborn, the state of the animation should be reset\\
    }

    // return the value that if the player is invincible
    public bool GetPlayerStatus()
    {
        return playerInvincible;
    }

    public void TakeDamage(int damage)
    {

        if (!playerInvincible)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            animator.SetTrigger("Attacked");
            CurrentHealth -= damage;
            healthBar.SetHealth(CurrentHealth);
            playerInvincible = true;
        }

    }

    // Killed by rock
    public void GetCrashed()
    {
        playerInvincible = false;
        animator.SetTrigger("Attacked");
        CurrentHealth = 0;
        healthBar.SetHealth(CurrentHealth);

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
        // float extraHeightText = 0.01f;
        bool raycastHit = Physics.Raycast(_rigidBody.position+new Vector3(0, 0.1f, 0), Vector3.down, extraHeightText, GroundLayerMask);
        Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x, _rigidBody.position.y - extraHeightText, _rigidBody.position.z), Color.red);
        return raycastHit;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        // FindObjectOfType<AudioManager>().Play("PlayerWalk");
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
        FindObjectOfType<AudioManager>().Play("PlayerJump");
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            FindObjectOfType<AudioManager>().Play("PlayerFire");
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
        // transform.Rotate(0f, 180f, 0f);
        SR.flipX = !SR.flipX;
        FirePoint.Rotate(0f, 180f, 0f, Space.Self);
        if (_isFacingRight)
            FirePoint.position += new Vector3(1.5f, 0, 0);
        else
            FirePoint.position -= new Vector3(1.5f, 0, 0);
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
        animator.SetTrigger("Respawn");
        transform.position = SavedPosition;
        CurrentHealth = MaxHealth;
        healthBar.SetHealth(CurrentHealth);
        GetComponent<PlayerInput>().enabled = true;
        // Application.LoadLevel(Application.loadedLevel);
    }
}