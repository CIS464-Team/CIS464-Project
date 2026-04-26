using UnityEngine;
using UnityEngine.InputSystem;

public class LaserReflector : MonoBehaviour
{
    public bool canMove = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && canMove)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame) transform.position += Vector3.up;
            if (Keyboard.current.sKey.wasPressedThisFrame) transform.position += Vector3.down;
            if (Keyboard.current.aKey.wasPressedThisFrame) transform.position += Vector3.left;
            if (Keyboard.current.dKey.wasPressedThisFrame) transform.position += Vector3.right;
        }
    }
}
