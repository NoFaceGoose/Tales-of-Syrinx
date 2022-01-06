using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public Rigidbody FragileClone;

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
        if (other.gameObject.CompareTag("Player") && other.gameObject.transform.position.y > gameObject.transform.position.y + other.gameObject.transform.localScale.y / 5)
        {
            Invoke("Destroy", 0.5f);
        }
    }

    void Destroy()
    {
        gameObject.SetActive(false);
        Invoke("Return", 3f);
    }

    void Return()
    {
        gameObject.SetActive(true);
    }
}
