using UnityEngine;

public class Area4ThirdPuzzleResetButton : MonoBehaviour
{
    public ThirdPuzzle puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
            puzzle.canReset = false;
            GetComponent<SpriteRenderer>().enabled = false;
    }
    void Update()
    {
        if (puzzle.canReset)
            GetComponent<SpriteRenderer>().enabled = true;
        else
            GetComponent<SpriteRenderer>().enabled = false;
    }
}

