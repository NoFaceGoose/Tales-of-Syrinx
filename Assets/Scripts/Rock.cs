using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock : MonoBehaviour
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

        if (GetComponent<Rigidbody>() != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Lose");
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Ground"))
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }
    }
}
