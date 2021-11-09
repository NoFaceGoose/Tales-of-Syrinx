using UnityEngine;

public class Key : MonoBehaviour
{
    public float TumbleSpeed;

    private bool keyB = false;
    private int keyIndex = 0;
    private Vector3 offset;
    private Transform playerTrans;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * TumbleSpeed;
        playerTrans = GameObject.FindWithTag("Player").transform;
        offset = new Vector3(-0.6f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (keyB == false)
        {
            if (rb.angularVelocity.z < 0.5)
            {
                rb.angularVelocity = Random.insideUnitSphere * TumbleSpeed;
            }
        }
        else
        {
            if (playerTrans != null)
            {
                switch (keyIndex)
                {
                    case 1:
                        transform.position = playerTrans.position + offset; break;
                    case 2:
                        transform.position = playerTrans.position + offset * 2; break;
                    case 3:
                        transform.position = playerTrans.position + offset * 3; break;
                    default: break;
                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyB = true;
            switch (PlayerController.player.keys)
            {
                case 0:
                    keyIndex = 1; PlayerController.player.keys++; break;
                case 1:
                    keyIndex = 2; PlayerController.player.keys++; break;
                case 2:
                    keyIndex = 3; PlayerController.player.keys++; break;
                default: break;
            }
        }
    }
}
