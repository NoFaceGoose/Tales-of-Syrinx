using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Lose");
        }
    }
}
