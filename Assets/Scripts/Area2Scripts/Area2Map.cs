using UnityEngine;

public class Area2Map : MonoBehaviour
{
    [Header("Note Closeup Sprite")]
    public GameObject closeUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            closeUp.SetActive(true);
            
            soundManager.Instance?.PlaySFX("Area2MapRustle");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            closeUp.SetActive(false);
        }
    }
}