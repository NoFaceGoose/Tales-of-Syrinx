using UnityEngine;

public class FallSwitch : MonoBehaviour
{
    public Rigidbody Rock;
    public Transform Fall;
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
            Rigidbody clone;
            clone = Instantiate(Rock, Fall.position, Fall.rotation);
            clone.velocity = transform.TransformDirection(0, 0, 0);
            Destroy(gameObject);
        }
    }
}