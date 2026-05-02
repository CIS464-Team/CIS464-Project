using UnityEngine;
using UnityEngine.InputSystem;

public class LaserReflector : MonoBehaviour
{
    public bool canMove = false;
    private BoxCollider2D boxCollider;
    private bool playerInRange = false;
    public float moveSpeed = 5f; // Units per second
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if(!canMove) {
            boxCollider.isTrigger = false;
        }
        if (transform.parent != null)
            targetPosition = transform.parent.position;
    }

    void Update()
    {
        if (canMove && playerInRange && !isMoving)
        {
            Vector3 direction = Vector3.zero;
            if (Keyboard.current.wKey.isPressed) direction = Vector3.up;
            else if (Keyboard.current.sKey.isPressed) direction = Vector3.down;
            else if (Keyboard.current.aKey.isPressed) direction = Vector3.left;
            else if (Keyboard.current.dKey.isPressed) direction = Vector3.right;

            if (direction != Vector3.zero && transform.parent != null)
            {
                targetPosition = transform.parent.position + direction;
                isMoving = true;
            }
        }

        if (isMoving && transform.parent != null)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.parent.position, targetPosition) < 0.01f)
            {
                transform.parent.position = targetPosition;
                isMoving = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
