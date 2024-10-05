using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    [SerializeField] private Rigidbody2D rb;

    public int Damage;

    public int slowEffect;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (hit) return;
        Vector2 Direction = Vector2.right;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(Direction*movementSpeed);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>()?.TakeDamage(1);
            //GetComponent<Health>()?.TakeDamage(Damage)
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    public virtual void Fire()
    {
        int turnmodifier = 1;
        if (FindObjectOfType<PlayerMovement>().turnedLeft)
        {
            turnmodifier = -1;
        }
        transform.localScale = new Vector2(turnmodifier, transform.localScale.y);
        rb.AddForce(transform.right*speed*turnmodifier);
    }



    private void Deactivate()
    {
        Destroy(gameObject);
    }
}