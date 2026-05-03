using UnityEngine;

public class statueSlot : MonoBehaviour
{
    public int requiredStatueID;
    public bool isOccupied = false;
    public int occupiedByID = -1;

//On enter check if statue is in right slot
    void OnTriggerEnter2D(Collider2D other)
    {
        //Every time statue enters check to see if it's right
        if (other.CompareTag("Statue"))
        {
            Statue statue = other.GetComponent<Statue>();
            isOccupied = true;
            occupiedByID = statue.statueID;
            PuzzleManager.instance.CheckPuzzle();
        }
    }

//on exit clear old statue's ID
    void OnExit2D(Collider2D other)
    {
        if (other.CompareTag("Statue"))
        {
            Statue statue = other.GetComponent<Statue>();
            if(statue != null && statue && !statue.IsMoving){

            isOccupied = false;
            occupiedByID = -1;
            PuzzleManager.instance.CheckPuzzle();
            }
        }
    }

//Finsl check to make sure all ID and slots match
    public void RegisterStatue(Statue statue)
    {
        isOccupied = true;
        occupiedByID = statue.statueID;
        PuzzleManager.instance.CheckPuzzle();
    }

    public bool IsCorrect()
    {
        return isOccupied && occupiedByID == requiredStatueID;
    }

}
