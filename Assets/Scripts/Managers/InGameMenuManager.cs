using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public static InGameMenuManager Instance;
    public GameObject menuCanvas;
    private bool menuOpen = false;
    public bool isMainMenuActive;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        menuCanvas.SetActive(false);
    }

    public void OpenSettings(InputValue value)
    {
        if(isMainMenuActive)
        {
            return;
        }
        // If the menu is CLOSED but the game is PAUSED we don't open the menu
        if(!menuOpen && PauseManager.IsGamePaused)
        {
            return;
        }
        // Otherwise, flip menu being open, pause the game if the menu is now open, unpause if we just closed it.
        menuOpen = !menuOpen;
        PauseManager.SetPause(menuOpen);
        menuCanvas.SetActive(menuOpen);
        print("ESC hit. InGameMenu is now " + menuOpen);
    }

    // Main Menu version of opening
    public void OpenSettings()
    {
        // open the menu
        menuCanvas.SetActive(true);
        menuOpen = true;
        print("InGameMenu opened by MainMenu Settings Button");
    }

    public void CloseSettings()
    {
        menuCanvas.SetActive(false);
        menuOpen = false;
        if(PauseManager.IsGamePaused)
        {
            PauseManager.SetPause(false);
        }
        print("X BUTTON hit. InGameMenu is now " + menuOpen);
        
    }

}
