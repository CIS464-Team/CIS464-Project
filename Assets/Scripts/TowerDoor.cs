using UnityEngine;
using UnityEngine.Tilemaps;
public class TowerDoor : MonoBehaviour
{
    public GameObject DoorClosed;
    public GameObject DoorOpen;
    public GameObject TheEndTP;

    void OnTriggerEnter2D(Collider2D other)
    {
        print("ALERT: OPENING THE FINAL DOOOR!! YESSS!!!");
        DoorClosed.SetActive(false);
        DoorOpen.SetActive(true);
        TheEndTP.SetActive(true);
    }
        
}
