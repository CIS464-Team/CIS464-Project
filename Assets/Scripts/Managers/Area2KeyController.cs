using UnityEngine;

public class Area2KeyController : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // pass
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        // check if we are in contact with a goal item
        if (collision.CompareTag("Player") )
        {
            // play the key obtain sound
            soundManager.Instance.PlaySFX("KeyChime");
            Debug.Log("Area 2 key obtained!");

            // remove the key
            gameObject.SetActive(false);
        }    
    }
}
