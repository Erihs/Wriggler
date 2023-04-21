using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }

    public Direction movementDirection;
    public float movementRange;
    public float movementSpeed;

    private Vector3 startingPosition;
    private Vector3 movementVector;
    private bool movingPositiveDirection = true;

    void Start()
    {
        startingPosition = transform.position;

        switch (movementDirection)
        {
            case Direction.Up:
                movementVector = Vector3.up;
                break;
            case Direction.Down:
                movementVector = Vector3.down;
                break;
            case Direction.Left:
                movementVector = Vector3.left;
                break;
            case Direction.Right:
                movementVector = Vector3.right;
                break;
        }
    }

    void Update()
    {
        if (movingPositiveDirection)
        {
            if (Vector3.Distance(transform.position, startingPosition + (movementVector * movementRange)) <= 0.1f)
            {
                movingPositiveDirection = false;
            }
            else
            {
                transform.Translate(movementVector * movementSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startingPosition) <= 0.1f)
            {
                movingPositiveDirection = true;
            }
            else
            {
                transform.Translate(-movementVector * movementSpeed * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(startingPosition + (movementVector * movementRange * 0.5f), new Vector3(1, 0.1f, 1) * movementRange);
        Gizmos.DrawLine(startingPosition, startingPosition + (movementVector * movementRange));
    }
}