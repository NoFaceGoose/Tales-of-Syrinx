using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public int RequiredKeys;
    public Sprite GateOpen;
    public GameObject Gate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (PlayerController.PlayerInstance.GetKeys() >= RequiredKeys)
            {
                if (Gate.GetComponent<SpriteRenderer>().sprite != GateOpen)
                {
                    FindObjectOfType<AudioManager>().Play("StoneDoor");
                    Gate.GetComponent<SpriteRenderer>().sprite = GateOpen;
                }
            }
        }
    }
}
