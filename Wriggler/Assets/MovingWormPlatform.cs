using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingWormPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;
    
    private SpriteRenderer spriteRenderer; // Added variable for the SpriteRenderer component
    void Start()
    {
        transform.position = points[startingPoint].position;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assign the SpriteRenderer component
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
            
            FlipSprite(); // Call the FlipSprite() method when hitting a waypoint
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
    
    private void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX; // Flip the sprite horizontally
        }
    }
}
