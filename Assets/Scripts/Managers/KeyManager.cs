using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KeyManager : MonoBehaviour
{
    // set up key data
    private KeyData keyData = new KeyData();
    // private GameObject[] keyObjects = new GameObject[4];
    [SerializeField] public GameObject[] keyObjects = new GameObject[4];
    [SerializeField] public GameObject[] hiddenKeys = new GameObject[4];

    void Start()
    {
        // init keys held
        keyData.keysHeld = new bool[4] {false, false, false, false};

        for (int i = 0; i < 4; i++)
        {
            keyObjects[i].SetActive(false);
        }
    }

    public bool[] GetKeysHeld()
    {
        return keyData.keysHeld;
    }

    public void SetKeysHeld(int id, bool value)
    {
        // check array bounds
        if (id >= 0 && id <= 3)
        {
            Debug.Log("Set key " + id + " to " + value);

            // update keydata
            keyData.keysHeld[id] = value;
            // swap hidden and new key values
            hiddenKeys[id].SetActive(!value);
            keyObjects[id].SetActive(value);
        }
        else
        {
            Debug.Log($"Key index {id} out of bounds (expected 0..3)");
        }
    }

    public void CheckAllKeys()
    {
        bool allKeys = true;

        for (int i = 0; i < 4; i++)
        {
            if (keyData.keysHeld[i] == false)
            {
                allKeys = false;
            }
        }

        if (allKeys == true)
        {
            // todo: handle opening door in central
        }
    }
}
