using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
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
        Debug.Log((other.transform.position - GameObject.FindWithTag("Key").transform.position).sqrMagnitude);
        if (other.CompareTag("Player"))
        {
            
            if ((other.transform.position - GameObject.FindWithTag("Key").transform.position).sqrMagnitude <= 2)
            {
                SceneManager.LoadScene("Victory");
            }
        }
    }
}
