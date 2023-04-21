using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 8f;
    private bool isFacingRight = true;
    private float horizontalInput;

    private Animator anim;

    //just for digging
    Vector2 movement;
    private bool isEarth;
    public float digSpeed = 5f;
    private bool isDigging = false;

    private bool isJumping;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(4f, 10f);

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.0f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (isEarth)
        {
            isDigging = true;
        }

        if(isDigging)//switches controls & animations if digging
        {
            DigUpdate();
            rb.gravityScale = 0f;
            //Debug.Log("No");
        }
        else
        {
            MovementUpdate();
            rb.gravityScale = 3f;
            //Debug.Log("Yes");
        }
    }

    private void FixedUpdate()
    {
        if(isDigging)
        {
            DigFxdUpdate();
            rb.gravityScale = 0f;
        }
        else
        {
            MovementFxdUpdate();
            rb.gravityScale = 3f;
        }
    }


    //Prevoiusly in update "movement"
    private void MovementUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter >= 0f && jumpBufferCounter >=0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetTrigger("jump");

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }
        

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        Flip();

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());

        //Fall Animation
        if (rb.velocity.y < 0 && !IsGrounded())
        {
            anim.SetBool("fall", true);
        }
        else
        {
            anim.SetBool("fall", false);
        }
    }
    
    //Prevoiusly in FixedUpdate "movement"
    private void MovementFxdUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
    }
    


    private bool IsGrounded()//Checks if the player is on the ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsWalled()//Checks if the player is on a wall
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    public float groundCheckRadius = 0.2f;
    public float wallCheckRadius = 0.2f;
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(wallCheck.position, wallCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontalInput != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
        
        anim.SetBool("IsWallSliding", isWallSliding);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -=Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()//Changes the directions which the player is facing(except digging)
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()//the time you have to jump after leaving a platform
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    
    //Digging Mechanic Update
    private void DigUpdate()
    {
        //Input for digging
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Blend tree components
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

    }
    
    //Digging Mechanic Fixed Update
    private void DigFxdUpdate()//moves player when digging
    {
        rb.MovePosition(rb.position + movement * digSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)//checks if player is in earth
    {
        if(collision.CompareTag("Earth"))
        {
            rb.gravityScale = 0f;
            isDigging = true;
            //Debug.Log("Digging");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//checks if player is not in earth
    {
        if(collision.CompareTag("Earth"))
        {
            rb.gravityScale = 3f;
            isDigging = false;
            //Debug.Log("NotDigging");
        }
    }
    
}