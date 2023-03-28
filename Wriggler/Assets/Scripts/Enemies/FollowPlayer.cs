using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    private bool isMovingLeft = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
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
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool("enemyAttack", true);
        }
        else
        {
            animator.SetBool("enemyAttack", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}