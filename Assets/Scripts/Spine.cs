using UnityEngine;

public class Spine : MonoBehaviour
{
    public int SpineDamage = 1;

    void OnCollisionEnter(Collision hitInfo)
    {
        PlayerController player = hitInfo.gameObject.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(SpineDamage);
        }
    }
}
