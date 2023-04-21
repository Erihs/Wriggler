using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;//stores last checkpoint
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        Reset();
    }

    public void Respawn()//send to last checkpoint
    {
        //TP player to checkpoint
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    private void Reset()//if "r" is pressed send to last checkpoint
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = currentCheckpoint.position;
            playerHealth.Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//Activates new checkpoints
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;

            //Debug.Log("respawn");
        }
    }
}
