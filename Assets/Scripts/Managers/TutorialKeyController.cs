using UnityEngine;

public class TutorialKeyController : MonoBehaviour
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
            // remove the gate and the key
            GameObject[] gateParts = GameObject.FindGameObjectsWithTag("TutorialGate");

            // play the gate opening sound
            soundManager.Instance.PlaySFX("gateopen");
            Debug.Log("gate opened");

            // if we have multiple gates, remove them all
            foreach (GameObject gate in gateParts)
            {
                gate.SetActive(false);
            }
            // remove the key itself
            gameObject.SetActive(false);
        }    
    }
}
