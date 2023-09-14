using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class cutSceneEnd : MonoBehaviour
{
    // The delay (in seconds) before changing to the next scene
    public float sceneChangeDelay = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Call the LoadNextScene function after the specified delay
        Invoke("LoadNextScene", sceneChangeDelay);
    }

    // Function to load the next scene
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Check if the next scene index is valid
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene by incrementing the current scene's build index
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.LogWarning("No more scenes to load. Make sure your build settings are set up correctly.");
        }
    }
}