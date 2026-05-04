using UnityEngine;

public class CloseToTower : MonoBehaviour
{
    private KeyManager keyManager;

    void Start()
    {
        keyManager = FindFirstObjectByType<KeyManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(keyManager.CheckAllKeys())
        {
            print("Player has 4 keys. THEY CAN OPEN THE DOOR!!!");
            var boxCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
            if (boxCollider != null)
                boxCollider.enabled = true;   
            var selfCollider = GetComponent<BoxCollider2D>();
            if (selfCollider != null)
                selfCollider.enabled = false;
        }
    }
}
