using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.CurrentHealth = PlayerController.PlayerInstance.MaxHealth;
            PlayerController.PlayerInstance.SavedPosition = transform.position;
        }
    }
}
