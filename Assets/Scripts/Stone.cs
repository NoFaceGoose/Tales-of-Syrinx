using UnityEngine;

public class Stone : MonoBehaviour
{
    public float Speed = -20f; // flying speed
    public int StoneDamage = 2; // damage as stone
    public int SoilDamage = 1; // damage as soil
    private int _damage; // damage
    public int Health = 3; // stone total health
    public int TurnPointHealth = 1; // turn to soil when health is less than this value
    public Rigidbody _rigidbody;
    public Animator Anim;

    private void Awake()
    {
        _damage = StoneDamage;
    }

    void Start()
    {
        if (_rigidbody != null)
        {
            _rigidbody.velocity = transform.right * Speed;
        }
    }

    void Update()
    {
        if (_damage == StoneDamage)
        {
            if (Health == TurnPointHealth)
            {
                _damage = SoilDamage;
                Anim.SetBool("IsHit", true);
            }

            if (Health == 0)
            {
                Anim.SetBool("IsBroken", true);
                if (_rigidbody != null)
                {
                    _rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
        else
        {
            if (Health == 0)
            {
                Anim.SetBool("IsSoilBroken", true);
                if (_rigidbody != null)
                {
                    _rigidbody.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayerController player = hitInfo.GetComponent<PlayerController>();

            if (player != null && !player.GetPlayerStatus())
            {
                player.TakeDamage(_damage);
                Health = 0;
                return;
            }
        }

        if (hitInfo.CompareTag("Bullet"))
        {
            Health--;
            Destroy(hitInfo.gameObject);
            return;
        }

        if (!hitInfo.CompareTag("Werewolf") && !hitInfo.CompareTag("StoneMan") && !hitInfo.CompareTag("EnemyMissile") && !hitInfo.CompareTag("CheckPoint"))
        {
            Health = 0;
        }
    }
    private void DestroyLater()
    {
        Invoke("Destroy", 0.25f);
        Destroy(gameObject.GetComponent<Rigidbody>());
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}