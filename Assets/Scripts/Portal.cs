using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject normalTree, portalTree;

    private void Start()
    {
        normalTree.SetActive(true);
        portalTree.SetActive(false);
    }

    public void SwitchTree()
    {
        normalTree.SetActive(false);
        portalTree.SetActive(true);
    }
}
