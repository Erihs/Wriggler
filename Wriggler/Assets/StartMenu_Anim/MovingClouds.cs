using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    public float moveSpeed = 0.2f; // speed at which clouds move to the left
    public float customRepeatWidth = 1.5f; // customizable repeat width in Unity Inspector
    private SpriteRenderer spriteRenderer; // reference to the sprite renderer component

    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / customRepeatWidth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // move the clouds to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // check if the clouds have looped back to their start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // fade out the clouds
            StartCoroutine(FadeOutClouds());

            // move the clouds back to their start position
            transform.position = startPos;

            // fade in the clouds
            StartCoroutine(FadeInClouds());
        }
    }

    IEnumerator FadeOutClouds()
    {
        // fade out the clouds over 0.5 seconds
        for (float i = 1f; i >= 0f; i -= Time.deltaTime * 2)
        {
            Color c = spriteRenderer.color;
            c.a = i;
            spriteRenderer.color = c;
            yield return null;
        }
    }

    IEnumerator FadeInClouds()
    {
        // fade in the clouds over 0.5 seconds
        for (float i = 0f; i <= 1f; i += Time.deltaTime * 2)
        {
            Color c = spriteRenderer.color;
            c.a = i;
            spriteRenderer.color = c;
            yield return null;
        }
    }

    // This method is called when the repeat width value is changed in the Unity Inspector
    void OnValidate()
    {
        repeatWidth = GetComponent<BoxCollider2D>().size.x / customRepeatWidth;
    }
}