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
        foreach (statueSlot slot in slots)
        {
            if(!slot.IsCorrect()) return;
        }

        door.SetActive(false);
        Debug.Log("Puzzle solved!");
    }


}
