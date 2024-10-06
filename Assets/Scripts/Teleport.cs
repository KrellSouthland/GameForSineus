using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject teleportPoint;
    private bool portalActive;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portalActive = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portalActive = false;
            player = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && portalActive)
        {
            player.transform.position = teleportPoint.transform.position;
        }
    }
}