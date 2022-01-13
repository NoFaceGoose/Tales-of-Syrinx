using UnityEngine;

public class FallSwitch : MonoBehaviour
{
    public GameObject Rock;
    public Sprite SpriteTriggered;
    private bool _isTriggered = false;

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
        if (!_isTriggered && (other.CompareTag("Player") || other.CompareTag("Bullet") || other.CompareTag("ReedPlatform")))
        {
            Rock.GetComponent<Rigidbody>().useGravity = true;
            Rock.GetComponent<Rigidbody>().isKinematic = false;
            _isTriggered = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteTriggered;
        }
    }
}