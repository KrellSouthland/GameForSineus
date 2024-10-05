using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public float outstup;
    private PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + outstup);
    }
}
