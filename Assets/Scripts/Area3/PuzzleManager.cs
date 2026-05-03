using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
   public static PuzzleManager instance;
   public statueSlot[] slots;
   public GameObject door;

    void Awake()
    {
        instance = this;
    }

    public void CheckPuzzle()
    {
        //For every statue slot check if it's in the right spot
        foreach (statueSlot slot in slots)
        {
            if(!slot.IsCorrect()) return;
        }

        //When solved deactivate door and play sound
        door.SetActive(false);
        Debug.Log("Puzzle solved!");
    }


}
