using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerShoot : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;

    public float returnDelay = 3f;
    private Transform player;
    private Vector2 originalPosition;
    private bool isReturning = false;
    private float returnTimer = 0f;

    public GameObject bullet;
    public GameObject bulletParent;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            isReturning = false; // Player is in line of sight, cancel return
            returnTimer = 0f;

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            FlipSprite();
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            anim.SetTrigger("shoot");
            nextFireTime = Time.time + fireRate;
        }
        else
        {
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

    private void Shoot()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void FlipSprite()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
