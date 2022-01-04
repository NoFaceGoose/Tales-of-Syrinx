using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator Anim;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.Recover(PlayerController.PlayerInstance.MaxHealth);
            PlayerController.PlayerInstance.SavedPosition = transform.position;
            Anim.SetBool("IsTriggered", true);
            if (IsInvoking("SlowDown"))
            {
                CancelInvoke("SlowDown");
            }
            Invoke("SlowDown", 1f);
        }
    }

    void SlowDown()
    {
        Anim.SetBool("IsTriggered", false);
    }
}
