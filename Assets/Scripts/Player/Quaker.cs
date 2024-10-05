using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaker : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

}
