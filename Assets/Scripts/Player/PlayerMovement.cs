﻿using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public bool turnedLeft {  get; private set; }

    [Header("Sounds")]
    [SerializeField] private AudioClip[] steps;
    [SerializeField] private AudioClip[] landing;

    private void Awake()
    {
        // Grab preferences for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // ПРОВЕРКА ДИАЛОГА
        if (!DialogueSystem.isOpen())
            horizontalInput = Input.GetAxis("Horizontal");
        else
            horizontalInput = 0;

        // flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
            turnedLeft = false;
        }

        if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            turnedLeft = true;
        }

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        
        // wall jump logic
        if (wallJumpCooldown > 0.2)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;
            // ПРОВЕРКА ДИАЛОГА
            if (Input.GetKey(KeyCode.Space) && !DialogueSystem.isOpen())
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCooldown = 0;

        }
    }
  
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        //return horizontalInput == 0 && isGrounded() && !onWall();
        return isGrounded() && !onWall() && !DialogueSystem.isOpen();
    }

    public void ChangeSpeed(int delta)
    {
        speed += delta;
    }

    public void ChangeJump(int delta)
    {
        jumpPower += delta;
    }

    public void SoundStep()
    {
        SoundManager.instance.PlaySound(steps[Random.Range(0, steps.Length)]);
    }
}
