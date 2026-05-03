using UnityEngine;

public class KeyArea : MonoBehaviour
{
    public bool tunnelOpen = false;
    public Sprite OpenTunnelSprite;
    public Sprite ClosedTunnelSprite;
    private SpriteRenderer tunnelSpriteRenderer;
    void Start()
    {
        // Grab the child tunnel's SpriteRenderer
        tunnelSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (tunnelSpriteRenderer != null)
        {
            tunnelSpriteRenderer.sprite = ClosedTunnelSprite;
            tunnelSpriteRenderer.sortingLayerName = "Collision";
            transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if(Area4Manager.Instance != null && Area4Manager.Instance.TunnelOpen)
        {
            tunnelOpen = true;
            OpenTunnel();
        }
    }

    public void OpenTunnel()
    {
        // Handle the logic for opening the tunnel
        tunnelSpriteRenderer.sprite = OpenTunnelSprite;
        tunnelSpriteRenderer.sortingLayerName = "WalkInFront";
        transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
        // Activate the rocks so it looks like the tunnel opened
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        
        
    }
}
