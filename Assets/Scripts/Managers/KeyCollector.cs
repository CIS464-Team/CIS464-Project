using System;
using UnityEngine;

public class KeyCollector : MonoBehaviour
{
    [SerializeField] public int keyID;
    private KeyManager keyManager;
    private bool keyState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // check to see if we collected this key already
        keyState = keyManager.GetKeysHeld()[keyID];

        if (keyState == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if we are in contact with a goal item
        if (collision.CompareTag("Player") )
        {
            Debug.Log("Key " + keyID + " picked up");

            // play the gate opening sound
            soundManager.Instance.PlaySFX("gateopen");

            // update keys held
            keyManager.SetKeysHeld(keyID, true);

            // remove the key itself
            gameObject.SetActive(false);
        }
    }
}
