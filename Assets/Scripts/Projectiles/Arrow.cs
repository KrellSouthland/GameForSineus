using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private float activeSpeed;
    private ArrowTrap trap;

    private bool flying;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            float movementSpeed = -1 * activeSpeed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);
    }

    public void ActivateProjectile()
    {
        flying = true;
        activeSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TrapActivator>())
        {
            return;
        }
        activeSpeed = 0;
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);

        }

        gameObject.SetActive(false);
        transform.position = trap.transform.position;
        trap.NextArrow();
    }

    public void FindTrap(ArrowTrap thisTrap)
    {
        trap = thisTrap;
    }
}
