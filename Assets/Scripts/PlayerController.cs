using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

<<<<<<< Updated upstream
    public float moveSpeed;
    public float jumpForce = 5.0f;
=======
    public float jumpForce = 10.0f;

>>>>>>> Stashed changes
    public float gravity;
    public GameObject Bullet;
    public int keys = 0;

    // jump function
    private Vector3 jump;
    // private int jumpCount = 0; //Make the player able to double jump
    private bool canDoubleJump = false; //Make the player able to double jump
    private Rigidbody rg;
    private float disToGround = 1.0f;
    private float inputX;
    private bool isGrounded = false;

<<<<<<< Updated upstream


    void Awake()
    {
        player = this;
    }
=======
    public bool canDoubleJump = false; //Make the player able to double jump
>>>>>>> Stashed changes

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rg.velocity = new Vector3(inputX * moveSpeed, rg.velocity.y, 0);
        Physics.gravity = new Vector3(0, gravity, 0);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, disToGround);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputX = value.ReadValue<Vector2>().x;
    }

    void jump()
    {
        rg.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (isGrounded)
        {
            Debug.Log("jump!");
            jump();
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            Debug.Log("double jump!");
            jump();
        }
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            GameObject shootingPoint = GameObject.Find("Fire");
            Instantiate(Bullet, shootingPoint.GetComponent<Transform>().position, shootingPoint.GetComponent<Transform>().rotation);
        }
    }
}
