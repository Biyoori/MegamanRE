using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Transform move;
    Object bulletRef;

    [SerializeField]
    Transform groundCheck;

    bool isGrounded = false;
    bool isAttack = false;
    bool shotLeft = false;

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
        bulletRef = Resources.Load("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        float translate = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        move.Translate(translate, 0, 0);
        if (translate < 0 && isGrounded)
        {
            shotLeft = false;
            if (isAttack)
            {
                animator.Play("Mega_RunAttack");
                spriteRenderer.flipX = true;
            }
            else
            {
                animator.Play("Mega_Run");
                spriteRenderer.flipX = true;
            }
        }
        else if (translate > 0 && isGrounded)
        {
            shotLeft = true;
            if (isAttack)
            {
                animator.Play("Mega_RunAttack");
                spriteRenderer.flipX = false;
            }
            else
            {
                animator.Play("Mega_Run");
                spriteRenderer.flipX = false;
            }
        }
        else
            if (isGrounded && !isAttack)
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

    void GroundCheck()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
            isGrounded = false;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isAttack)
        {
            isAttack = true;
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            if (shotLeft)
            {
                bullet.transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, -1);
                bullet.GetComponent<BulletScript>().direction = new Vector2(3, 0);
            }

            else
            {
                bullet.transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, -1);
                bullet.GetComponent<BulletScript>().direction = new Vector2(-3, 0);
            }
                

        }
        if (isAttack)
        {
            animator.Play("Mega_Attack");
            Invoke("StopAttack", 0.2f);
        }
    }

    void StopAttack()
    {
        isAttack = false;
    }
}
