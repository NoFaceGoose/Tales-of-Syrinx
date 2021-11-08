using UnityEngine;
using UnityEngine.SceneManagement;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && transform.position.y > 1.8)
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Lose");
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
