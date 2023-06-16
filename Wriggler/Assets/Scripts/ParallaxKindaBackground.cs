using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxKindaBackground : MonoBehaviour
{
    public float parallaxFactor = 0.5f; // Controls the parallax effect. Increase for stronger effect.
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        deltaMovement.y = 0f; // Set Y-axis delta movement to zero
        transform.position += deltaMovement * parallaxFactor;
        lastCameraPosition = cameraTransform.position;
    }
}