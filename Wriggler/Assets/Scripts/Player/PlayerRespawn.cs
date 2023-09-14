using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint; // stores the last checkpoint
    private Health playerHealth;
    private AudioSource audioSource; // Reference to the AudioSource component

    public AudioClip checkpointSound; // Assign the checkpoint sound effect in the Inspector

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void Update()
    {
        Reset();
    }

    public void Respawn()
    {
        // Teleport the player to the checkpoint
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    private void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = currentCheckpoint.position;
            playerHealth.Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;

            // Get the CheckpointGlow script and start the glowing effect
            checkpointGlow checkpointGlow = collision.GetComponent<checkpointGlow>();
            if (checkpointGlow != null)
            {
                checkpointGlow.StartGlow();
            }

            // Play the checkpoint sound effect
            if (checkpointSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(checkpointSound);
            }
        }
    }
}