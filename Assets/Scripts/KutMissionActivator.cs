using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class KutMissionActivator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("LOG LOG LOG");
        if (collision.tag == "Player")
        {
           
            KutMission.ActivateMission();
            Debug.Log("ACtivate MIssion!!");
            Destroy(gameObject);
        }
    }
     
}
