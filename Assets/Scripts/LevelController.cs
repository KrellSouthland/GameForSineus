using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    private Portal portal;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        portal = FindObjectOfType<Portal>();
    }

    public void ActivatePortal()
    {
        portal.SwitchTree();
    }

}
