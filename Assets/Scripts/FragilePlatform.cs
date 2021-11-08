using UnityEngine;

public class FragilePlatform : MonoBehaviour
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
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y > gameObject.transform.position.y)
        {
            Invoke("Destroy", 0.5f);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
