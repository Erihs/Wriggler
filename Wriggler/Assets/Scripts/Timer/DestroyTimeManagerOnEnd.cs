using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimeManagerOnEnd : MonoBehaviour
{
    void Start()
    {
        // Find the TimeManager object in the scene and destroy it
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
        {
            Destroy(timeManager.gameObject);
        }
    }
}
