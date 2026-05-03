using System.Collections;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [Header("References")]
    public Area2Manager manager; 
    private SpriteRenderer sr;
    private Collider2D col;
    private bool isTransitioning = false;

    // Called by Scene Manager when the player steps on a wrong tile
    public void ShowButton()
    {
        // Grab component references if they haven't been assigned
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (col == null) col = GetComponent<Collider2D>();

        // Make the button visible + interactive
        gameObject.SetActive(true);
        sr.enabled = true;
        col.enabled = true;

        // Reset size to 0.25
        transform.localScale = new Vector3(0.25f, 0.25f, 1f); 

        // Opacity to 70%
        sr.color = new Color(1f, 1f, 1f, 0.7f); 

        // Make sure button is facing the correct direction
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(AnimateAndReset());
        }
    }

    // Handles the button's spin/fade effect
    IEnumerator AnimateAndReset()
    {
        isTransitioning = true;
        col.enabled = false; // Disable collisions to prevent multiple triggers

        float duration = 1f;  // Animation duration
        float elapsed = 0f;     // Timer to track progress

        // Animation loop
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;

            // Rotate the button 720 degrees
            transform.Rotate(0, 0, 720 * Time.deltaTime);

            // Fade the color alpha from 0.7 down to 0
            sr.color = new Color(0f, 0f, 1f, 0.7f * (1 - percent));

            // Shrink the scale from 0.25 to 0
            if (transform.localScale.x > 0)
                transform.localScale = Vector3.one * (0.25f * (1 - percent));

            yield return null; // Wait for the next frame
        }

        // Signal the Scene Manager reset puzzle tiles
        manager.ResetPuzzle();
        
        // Reset button state
        isTransitioning = false;
        gameObject.SetActive(false); 
    }
}