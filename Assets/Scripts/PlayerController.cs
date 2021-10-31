using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    public Rigidbody rg;
    private float inputX;
    private bool isGrounded = false;
    public float disToGround = 1f;
    public GameObject Bullet;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rg.velocity = new Vector2(inputX * moveSpeed, rg.velocity.y);
        Physics.gravity = new Vector3(0, gravity, 0);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, disToGround);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputX = value.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (isGrounded && value.performed)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
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
