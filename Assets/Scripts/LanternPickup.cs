using UnityEngine;

public class LanternPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PickupLantern(other.gameObject);
        }
    }

    void PickupLantern(GameObject player)
    {
        Debug.Log("Lantern picked up!");
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.zero;
        // Set sorting layer to decor so it appears on top of the player
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Decor";
        }
    }
}
