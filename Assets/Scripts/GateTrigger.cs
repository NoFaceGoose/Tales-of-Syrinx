using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public int RequiredKeys;
    public Sprite GateOpen;
    public GameObject Gate;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (PlayerController.PlayerInstance.GetKeys() >= RequiredKeys)
            {
                Gate.GetComponent<SpriteRenderer>().sprite = GateOpen;
            }
        }
    }
}
