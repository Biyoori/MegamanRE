using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Transform move;

    [SerializeField]
    Transform groundCheck;

    bool isGrounded = false;

    public float moveSpeed = 5f;
    public float jumpPower = 4f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        move = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
        Move();
        Jump();
        
    }

    void Move()
    {
        float translate = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        move.Translate(translate, 0, 0);
        Debug.Log("Move: " + move);
        Debug.Log("Translate " + translate);
        if (translate < 0 && isGrounded)
        {
            animator.Play("Mega_Run");
            spriteRenderer.flipX = true;
        }
        else if (translate > 0 && isGrounded)
        {
            animator.Play("Mega_Run");
            spriteRenderer.flipX = false;
        }
        else
            if (isGrounded)
            animator.Play("Mega_Idle");
    }

    void Jump()
    {
        if (!isGrounded)
            animator.Play("Mega_Jump");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpPower;
        }

        if (rb2d.velocity.y < 0)
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }
}
