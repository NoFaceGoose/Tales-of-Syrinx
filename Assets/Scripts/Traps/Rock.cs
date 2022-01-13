using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject RockPrefab;
    public bool Falling = false;
    public static Rock RockTrigger;

    void Awake()
    {
        RockTrigger = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Falling && RockPrefab.GetComponent<Rigidbody>() != null)
        {
            RockPrefab.GetComponent<Rigidbody>().useGravity = true;
            RockPrefab.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!Falling)
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
                if (RockPrefab.GetComponent<Rigidbody>() != null)
                {
                    Destroy(RockPrefab.GetComponent<Rigidbody>());
                }

            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                PlayerController.PlayerInstance.GetCrashed();
                return;
            }

            if (other.CompareTag("ReedPlatform"))
            {
                Destroy(gameObject);
                return;
            }

            if (other.CompareTag("Ground"))
            {
                Destroy(RockPrefab.gameObject);
            }

            if (other.CompareTag("Rock"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
