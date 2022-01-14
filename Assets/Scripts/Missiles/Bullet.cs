using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 20f;
    public Rigidbody _rigidbody;

    public int BulletDamage = 1;

    void Start()
    {
        _rigidbody.velocity = transform.right * Speed;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.CompareTag("StoneMan"))
        {
            StoneMan stoneMan = hitInfo.GetComponent<StoneMan>();
            stoneMan.TakeDamage(BulletDamage);
        }
        if (hitInfo.CompareTag("Werewolf"))
        {
            Werewolf werewolf = hitInfo.GetComponent<Werewolf>();
            werewolf.TakeDamage(BulletDamage);
        }
        if (!hitInfo.CompareTag("Player") && !hitInfo.CompareTag("EnemyMissile") && !hitInfo.CompareTag("CheckPoint"))
        {
            Destroy(gameObject);
        }
        if (hitInfo.CompareTag("Prince"))
        {
            hitInfo.GetComponent<PrinceController>().TakeDamage(BulletDamage);
        }
    }
}