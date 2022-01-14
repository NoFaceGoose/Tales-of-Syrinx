using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator Anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.PlayerInstance.Recover(1);
            PlayerController.PlayerInstance.SavedPosition = new Vector3(transform.position.x, transform.position.y, 0f);
            Anim.SetBool("IsTriggered", true);
            FindObjectOfType<AudioManager>().Play("CheckPoint");

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
