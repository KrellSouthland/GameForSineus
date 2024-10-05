using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    private Transform[] targets = new Transform[2];
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    private Transform nextTarget;

    [Header("Movement parameters")]
    [SerializeField] private float speed;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;
    //private Rigidbody2D rb;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        targets[0] = leftEdge;
        targets[1] = rightEdge;
        nextTarget = targets[0];
    }

    public void MoveToTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        transform.Rotate(0, 180, 0);
    }

    private void ChangeTarget()
    {
        for (int i = 0; i< targets.Length;i++)
        {
            if (nextTarget != targets[i])
            {
                nextTarget = targets[i];
                break;
            }
        }
    }

    private void Update()
    {
        MoveToTarget(nextTarget);
        if (transform.position.x==nextTarget.position.x)
        {
            ChangeDirection();
            ChangeTarget();
        }
    }

}
