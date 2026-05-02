using UnityEngine;

public class Area4FinalPuzzleResetButton : MonoBehaviour
{
    public FinalPuzzle puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
    }
}

