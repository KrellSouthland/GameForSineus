using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaker : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            try { collision.GetComponent<MeleeEnemy>().Root(delay); } catch { }
            try { collision.GetComponent<RangedEnemy>().Root(delay); } catch { }
        }
    }

}
