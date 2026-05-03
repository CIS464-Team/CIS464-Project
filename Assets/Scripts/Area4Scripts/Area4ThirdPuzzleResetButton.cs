using UnityEngine;
using System.Collections;

public class Area4ThirdPuzzleResetButton : MonoBehaviour
{
    public ThirdPuzzle puzzle;
    public SpriteRenderer sr;
    public Collider2D col;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
            puzzle.canReset = false;
            StartCoroutine(AnimateAndReset());
    }
    void Update()
    {
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (col == null) col = GetComponent<Collider2D>();
        if (puzzle.canReset)
            sr.enabled = true;
    }

    IEnumerator AnimateAndReset()
    {
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

            // Shrink the scale from 1 to 0
            if (transform.localScale.x > 0)
                transform.localScale = Vector3.one * (1f * (1 - percent));

            yield return null; // Wait for the next frame
        }

        sr.enabled = false; // Hide the button after animation
        sr.color = new Color(1f, 1f, 1f, 0.7f); // Reset color for next time
        transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale for next time
        col.enabled = true; // Re-enable collisions for next time
    }
}

