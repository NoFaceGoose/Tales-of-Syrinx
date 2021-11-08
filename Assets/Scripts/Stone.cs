using UnityEngine;
using UnityEngine.SceneManagement;

public class Stone : MonoBehaviour
{
    private int zeroVelocityCount = 0;
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
                Destroy(other.gameObject);
                SceneManager.LoadScene("Lose");
            }
            if (other.gameObject.CompareTag("Platform"))
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }

    }
}
