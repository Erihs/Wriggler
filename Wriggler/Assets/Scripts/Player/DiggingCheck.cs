using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingCheck : MonoBehaviour
{
    private bool isEarth;
    private bool isDigging;

    public Digging script;
    public PlayerMovement script1;


    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        if (isEarth)
        {
            isDigging = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDigging)
        {
            rb.gravityScale = 0f;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Earth"))
        {
            Debug.Log("Digging");
            rb.gravityScale = 0f;
            script.enabled = true;
            script1.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Earth"))
        {
            rb.gravityScale = 2f;
            script.enabled = false;
            script1.enabled = true;
            
            Debug.Log("NotDigging");
        }
    }
}
