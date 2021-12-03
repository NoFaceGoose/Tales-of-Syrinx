using UnityEngine;
using UnityEngine.SceneManagement;

public class PassTutorial : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {

            if (PlayerController.PlayerInstance.GetKeys() >= 1)
            {
                SceneManager.LoadScene("Level1");
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
