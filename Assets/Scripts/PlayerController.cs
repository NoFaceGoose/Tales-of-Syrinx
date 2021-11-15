using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PlayerInstance;

    public float MoveSpeed;
    public float JumpForce = 5.0f;
    public float Gravity;

    private int _keys = 0;

    // jump function
    private Vector3 _jump;
    // private int jumpCount = 0; //Make the player able to double jump
    private bool _canDoubleJump = false; //Make the player able to double jump
    private Rigidbody _rigidBody;
    private float _disToGround = 1.0f;
    private float _inputX;
    private bool _isGrounded = false;

    // Flip
    private bool _isFacingRight = true;

    // Fire
    public Transform FirePoint;
    public GameObject BulletPrefab;

    void Awake()
    {
        PlayerInstance = this;
    }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
    void Update()
    {
        _rigidBody.velocity = new Vector3(_inputX * MoveSpeed, _rigidBody.velocity.y, 0);
        Physics.gravity = new Vector3(0, Gravity, 0);
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _disToGround);
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
        if (_isGrounded)
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, 0);
            // rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            _rigidBody.AddForce(_jump * JumpForce, ForceMode.Impulse);
            _canDoubleJump = true;
        }
        else if (_canDoubleJump)
        {
            Debug.Log("double jump!");
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0, 0);
            // rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            _rigidBody.AddForce(_jump * JumpForce, ForceMode.Impulse);
            _canDoubleJump = false;
        }
        // if (isGrounded)
        // {
        //     jumpCount = 0;
        // }
        // if (jumpCount < 3)
        // {
        //     rg.velocity = new Vector2(rg.velocity.x, jumpForce);
        //     jumpCount += 1;
        // }
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        // if (value.performed)
        // {
        //     GameObject shootingPoint = GameObject.Find("Fire");
        //     Instantiate(Bullet, shootingPoint.GetComponent<Transform>().position, shootingPoint.GetComponent<Transform>().rotation);
        // }
        if (value.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // shooting logic
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);

    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public int SetKeys()
    {
        int keyIndex = 0;
        switch (_keys)
        {
            case 0:
                keyIndex = 1; break;
            case 1:
                keyIndex = 2; break;
            case 2:
                keyIndex = 3; break;
            default: break;
        }
        _keys++;
        return keyIndex;
    }

    public int GetKeys()
    {
        return _keys;
    }
}