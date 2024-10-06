using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject teleportPoint;
 
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.O) && other.gameObject.name == "Shaman"){
            other.transform.position = teleportPoint.transform.position;
        }
    }
}