using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public int EnemyBulletDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(Speed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        // Debug.Log(hitInfo.name);
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(EnemyBulletDamage);
        }
        Destroy(gameObject);
    }
}