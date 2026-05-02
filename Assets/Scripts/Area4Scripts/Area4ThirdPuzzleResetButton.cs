using UnityEngine;

public class Area4ThirdPuzzleResetButton : MonoBehaviour
{
    public ThirdPuzzle puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            puzzle.ResetPuzzle();
    }
}

