using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    Transform leftWaypoint, rightWaypoint;
    Vector3 localScale;
    bool movingRight = true;
    Rigidbody2D rb;

    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        leftWaypoint = GameObject.Find ("leftWaypoint").GetComponent<Transform>();
        rightWaypoint = GameObject.Find ("rightWaypoint").GetComponent<Transform>();
    }

    void Update()
    {
        if (transform.position.x > rightWaypoint.position.x)
        {
            movingRight = false;
        }
        if (transform.position.x < leftWaypoint.position.x)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            moveRight();
        }
        else
        {
            moveLeft();
        }

        
    }

    void moveRight()
    {
        movingRight = true;
        localScale.x = 1;
        transform.localScale = localScale;
        rb.velocity = new Vector2 (localScale.x * moveSpeed, rb.velocity.y);
    }

    void moveLeft()
    {
        movingRight = false;
        localScale.x = -1;
        transform.localScale = localScale;
        rb.velocity = new Vector2 (localScale.x * moveSpeed, rb.velocity.y);
    }
}
