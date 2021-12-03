using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyFire;
    public Transform Left;
    public Transform Right;
    public float WalkSpeed;
    public bool OnPatrol;
    public bool TowardsLeft;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("Fire", 0.5f, 1.0f);
    }

    void FixedUpdate()
    {
        if (OnPatrol)
        {
            if (TowardsLeft == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * WalkSpeed);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * WalkSpeed);
            }
        }
    }

    void Update()
    {
        if (OnPatrol)
        {
            if (transform.position.x <= Left.position.x)
            {
                TowardsLeft = false;
            }
            if (transform.position.x >= Right.position.x)
            {
                TowardsLeft = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lose");
        }
    }

    void Fire()
    {
        Instantiate(EnemyBullet, EnemyFire.position, EnemyFire.rotation);
    }
}
