using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    public int SpineDamage = 1;

    void OnTriggerEnter(Collider hitInfo)
    {
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null && !player.GetPlayerStatus())
        {
            player.TakeDamage(SpineDamage);
        }
    }
}
