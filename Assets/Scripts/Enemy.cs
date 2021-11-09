using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    public Transform EnemyFire;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Lose");
        }
    }

    public void Fire()
    {
        Instantiate(EnemyBullet, EnemyFire.position, EnemyFire.rotation);
    }
}
