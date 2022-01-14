using UnityEngine;

public class GenerateRocks : MonoBehaviour
{
    public GameObject GeneratePosition;
    public GameObject RockPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InvokeRepeating("Generate", 0f, 0.6f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("Generate");
        }
    }

    void Generate()
    {
        Instantiate(RockPrefab, GeneratePosition.transform.position, GeneratePosition.transform.rotation);
        Rock.RockTrigger.Falling = true;
        FindObjectOfType<AudioManager>().Play("StoneFall");
    }
}
