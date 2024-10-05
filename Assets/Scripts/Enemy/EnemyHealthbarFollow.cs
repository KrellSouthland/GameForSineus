using UnityEngine;

public class EnemyHealthbarFollow : MonoBehaviour
{
    public float outstup = 1f;
    [SerializeField] private Transform enemy;

    void Start()
    {
        enemy = transform.parent;
        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (enemy.gameObject.activeInHierarchy)
        {
            transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + outstup);
        } 
        else
        {
            Destroy(gameObject);
        }
    }
}
