using UnityEngine;

public class FallSwitch : MonoBehaviour
{
    public GameObject Rock;

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
            Rock.GetComponent<Rigidbody>().useGravity = true;
            Rock.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(gameObject);
        }
    }
}