using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss BossInstance;

    public GameObject BossBullet;
    public Transform EnemyFire;
    public float JumpSpeed;
    public int Health;
    public Material MatTwo;
    public Material MatThree;
    public int CollisionDamage = 1;

    private Rigidbody _rigidBody;
    private Vector3 _jumpForce;


    void Awake()
    {
        BossInstance = this;
        _rigidBody = GetComponent<Rigidbody>();
        _jumpForce = new Vector3(0, JumpSpeed, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 0.8f);
        InvokeRepeating("Jump", 0f, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(CollisionDamage);
        }
    }

    public void Fire()
    {
        Instantiate(BossBullet, EnemyFire.position, EnemyFire.rotation);
    }

    public void Jump()
    {
        _rigidBody.AddForce(_jumpForce, ForceMode.Impulse);
    }

    public void Improve()
    {
        Health -= 1;
        CancelInvoke();
        switch (Health)
        {
            case 2:
                transform.GetComponent<Renderer>().material = MatTwo; InvokeRepeating("Fire", 0f, 0.6f);
                InvokeRepeating("Jump", 0f, 1.2f); break;
            case 1:
                transform.GetComponent<Renderer>().material = MatThree; InvokeRepeating("Fire", 0f, 0.4f);
                InvokeRepeating("Jump", 0f, 1f); break;
            case 0: Destroy(gameObject); break;
            default: break;
        }
    }
}