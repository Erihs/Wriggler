using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float returnDelay = 2f;
    private Transform player;
    private bool isMovingLeft = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 originalPosition;
    private bool isReturning = false;
    private float returnTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            isReturning = false; // Player is in line of sight, cancel return
            returnTimer = 0f;

            Vector2 direction = player.position - transform.position;
            if (direction.x < 0 && !isMovingLeft)
            {
                isMovingLeft = true;
                spriteRenderer.flipX = true;
            }
            else if (direction.x > 0 && isMovingLeft)
            {
                isMovingLeft = false;
                spriteRenderer.flipX = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool("enemyAttack", true);
        }
        else
        {
            animator.SetBool("enemyAttack", false);
            if (!isReturning)
            {
                // Player is out of line of sight, start return timer
                returnTimer += Time.deltaTime;
                if (returnTimer >= returnDelay)
                {
                    isReturning = true;
                    returnTimer = 0f;
                }
            }
            else
            {
                // Return to original position
                transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, originalPosition) < 0.01f)
                {
                    isReturning = false;
                    transform.position = originalPosition;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}