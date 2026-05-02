using UnityEngine;

public class Area4FirstPuzzleResetButton : MonoBehaviour
{
    public FirstPuzzle puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
    }
}

