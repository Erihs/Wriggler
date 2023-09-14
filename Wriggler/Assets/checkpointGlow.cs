using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointGlow : MonoBehaviour
{
    public float glowSpeed = 1.0f; // Adjust the speed of the glowing effect in the Inspector.
    public Color glowColor = Color.white; // The color to transition to (white).

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isGlowing = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isGlowing)
        {
            // Calculate the lerp value based on time and speed
            float lerpValue = Mathf.PingPong(Time.time * glowSpeed, 1f);

            // Lerp between the original color and the glow color
            spriteRenderer.color = Color.Lerp(originalColor, glowColor, lerpValue);
        }
    }

    public void StartGlow()
    {
        isGlowing = true;
    }

    public void StopGlow()
    {
        isGlowing = false;
        spriteRenderer.color = originalColor;
    }
}