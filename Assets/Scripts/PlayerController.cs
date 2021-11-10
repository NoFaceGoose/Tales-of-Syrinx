using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    public float moveSpeed;
    public float jumpForce = 5.0f;
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

    // Flip
    private bool isFacingRight = true;



    void Awake()
    {
        player = this;
    }

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
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

        // Flip the character
        if(inputX > 0 && !isFacingRight)
        {
            Flip();
        } 
        else if(inputX < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (isGrounded)
        {
            rg.velocity = new Vector3(rg.velocity.x, 0, 0);
            // rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            rg.AddForce(jump * jumpForce, ForceMode.Impulse);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            Debug.Log("double jump!");
            rg.velocity = new Vector3(rg.velocity.x, 0, 0);
            // rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            rg.AddForce(jump * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
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
        if (value.performed)
        {
            GameObject shootingPoint = GameObject.Find("Fire");
            Instantiate(Bullet, shootingPoint.GetComponent<Transform>().position, shootingPoint.GetComponent<Transform>().rotation);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
