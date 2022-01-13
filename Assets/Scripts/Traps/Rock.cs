using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject RockPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.GetCrashed();
            return;
        }


        if (other.CompareTag("Werewolf"))
        {
            other.GetComponent<CapsuleCollider>().enabled = false;
            other.GetComponent<Werewolf>().Die();
            return;
        }


        if (other.CompareTag("StoneMan"))
        {
            other.GetComponent<CapsuleCollider>().enabled = false;
            other.GetComponent<StoneMan>().Die();
            return;
        }


        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.GetCrashed();
            return;
        }

        if (other.CompareTag("Thorn"))
        {
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("ReedPlatform"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Platform") || other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Destroy(RockPrefab.GetComponent<Rigidbody>());
        }
    }
}
