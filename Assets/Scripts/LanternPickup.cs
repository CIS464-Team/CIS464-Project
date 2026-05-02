using UnityEngine;

public class LanternPickup : MonoBehaviour
{
    // When the player collides with the lantern, "pick it up"
    // by making it a child of the player and setting its position to the player's position
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickupLantern(other.gameObject);
        }
    }

    public void PickupLantern(GameObject player)
    {
        Debug.Log("Lantern picked up!");
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.zero;
        // Set sorting layer to player, but above the player so it appears on top of the player
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Player";
            sr.sortingOrder = 1; 
        }
    }
}
