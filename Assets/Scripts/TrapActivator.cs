using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] private ArrowTrap activatingTrap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activatingTrap.ClickActivate(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            activatingTrap.ClickActivate(false);
        }
    }
}
