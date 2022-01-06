using UnityEngine;

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
                if (gameObject.GetComponent<Rigidbody>().useGravity && !gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    PlayerController.PlayerInstance.Die();
                    return;
                }
            }

            if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("Ground"))
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }
    }
}
