using UnityEngine;

public class statueSlot : MonoBehaviour
{
    public int requiredStatueID;
    public int slotOrder;
    public bool isOccupied = false;
    public int occupiedByID = -1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Statue"))
        {
            Statue statue = other.GetComponent<Statue>();
            isOccupied = true;
            occupiedByID = statue.statueID;
            PuzzleManager.instance.CheckPuzzle();
        }
    }

    void OnExit2D(Collider2D other)
    {
        if (other.CompareTag("Statue"))
        {
            isOccupied = false;
            occupiedByID = -1;
            PuzzleManager.instance.CheckPuzzle();
        }
    }

    public bool IsCorrect()
    {
        return isOccupied && occupiedByID == requiredStatueID;
    }

}
