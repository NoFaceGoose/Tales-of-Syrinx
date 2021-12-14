using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        // code below allow cam only move on axis x
        newPosition.y = transform.position.y; 
        newPosition.z = transform.position.z;
        newPosition.x = newPosition.x + 7; // let player on the left corner of the map
        transform.position = newPosition;
    }
}
