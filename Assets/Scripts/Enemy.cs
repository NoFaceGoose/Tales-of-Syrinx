using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyFire", 0.05f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Lose");
        }
    }

    public void EnemyFire()
    {
        GameObject shootingPoint = GameObject.Find("EnemyFire");
        Instantiate(EnemyBullet, shootingPoint.GetComponent<Transform>().position, shootingPoint.GetComponent<Transform>().rotation);
    }
}
