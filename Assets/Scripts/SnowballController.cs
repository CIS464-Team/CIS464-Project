using UnityEngine;

public class SnowballController : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if we are in contact with a goal item
        if (collision.collider.tag == "Goal" )
        {
            if (collision.gameObject.tag == "Goal")
            {
                // remove the goal and the snowball
                collision.gameObject.SetActive(false);
                gameObject.SetActive(false);

                //play goal met sound
                soundManager.Instance.PlaySFX("goal");

                Debug.Log("Removed goal object");
            }
            else
            {
                Debug.Log("Warning: Attempted to remove a non-goal object");
            }
            
        }
        else
        {
            soundManager.Instance?.PlaySFX("SnowPush");
        }
    }
}
