using UnityEngine;

public class Thorn : MonoBehaviour
{
    public int ThornDamage = 1;

    void OnCollisionEnter(Collision hitInfo)
    {
        PlayerController player = hitInfo.gameObject.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(ThornDamage);
        }
    }
}
