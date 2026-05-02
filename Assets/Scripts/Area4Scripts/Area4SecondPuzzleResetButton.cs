using UnityEngine;

public class Area4SecondPuzzleResetButton : MonoBehaviour
{
    public SecondPuzzle puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
    }
}

