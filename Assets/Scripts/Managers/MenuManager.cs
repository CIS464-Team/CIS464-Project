using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    private bool mainMenuLoaded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // check if the main menu scene is loaded
        mainMenuLoaded = SceneManager.GetSceneByName("MainMenu").isLoaded;

        // menu opening logic when in normal play (open or close)
        if (!mainMenuLoaded)
        {
            // check menu activation key
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // flip active state
                menuCanvas.SetActive(!menuCanvas.activeSelf);
            }
        }
        // menu opening logic when in main menu (close)
        else
        {
            // check menu activation key
            if (Keyboard.current.escapeKey.wasPressedThisFrame && menuCanvas.activeSelf == true)
            {
                // close menu
                menuCanvas.SetActive(!menuCanvas.activeSelf);
            }
        }
        
    }

    // link this one to the main menu button
    public void OpenSettings()
    {
        // open the menu
        menuCanvas.SetActive(true);
    }

}
