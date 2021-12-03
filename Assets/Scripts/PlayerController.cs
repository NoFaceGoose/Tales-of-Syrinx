using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PlayerInstance;

    public float MoveSpeed;
    public float jumpForce = 5.0f;
    public float Gravity;

    private int _keys = 0;

    // jump function
    public LayerMask GroundLayerMask; // only check ground level
    private Vector3 _jump;
    private int jumpCount = 2; //Make the player able to double jump
    private Rigidbody _rigidBody;
    private float _inputX;

    // Flip
    private bool _isFacingRight = true;

    // Fire
    public Transform FirePoint;
    public GameObject BulletPrefab;

    // Launch the reed
    public GameObject ReedPrefab;

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

    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector3(_inputX * MoveSpeed, _rigidBody.velocity.y, 0);
        Physics.gravity = new Vector3(0, Gravity, 0);


        // if is on ground, can double jump
        if(GroundCheck())
        {
            jumpCount = 2;
        }
        // _isGrounded = Physics.Raycast(transform.position, Vector3.down, _disToGround);
    }

    private bool GroundCheck()
    {
        float extraHeightText = 2;
        bool raycastHit = Physics.Raycast(_rigidBody.position, Vector3.down, extraHeightText, GroundLayerMask);
        Debug.DrawLine(_rigidBody.position, new Vector3(_rigidBody.position.x, _rigidBody.position.y-2, _rigidBody.position.z), Color.red);
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
        if(value.performed && jumpCount > 0)
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
        if(value.performed)
        {
            Instantiate(ReedPrefab, FirePoint.position, FirePoint.rotation);
        }
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