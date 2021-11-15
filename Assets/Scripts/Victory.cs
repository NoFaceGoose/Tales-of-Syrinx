using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject KeyReminder;
    // Start is called before the first frame update
    void Start()
    {
        KeyReminder.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log((other.transform.position - GameObject.FindWithTag("Key").transform.position).sqrMagnitude);
        if (other.CompareTag("Player"))
        {

            if (PlayerController.PlayerInstance.GetKeys() >= 3)
            {
                SceneManager.LoadScene("Victory");
            }
            else
            {
                KeyReminder = GameObject.Find("Key Reminder");
                KeyReminder.GetComponent<Renderer>().enabled = true;
                CancelInvoke();
                Invoke("RemindDisappear", 2f);
            }
        }
    }

    void RemindDisappear()
    {
        KeyReminder.GetComponent<Renderer>().enabled = false;
    }
}
