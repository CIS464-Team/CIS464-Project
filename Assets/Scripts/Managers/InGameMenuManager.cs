using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    private bool menuOpen = false;
    public bool isMainMenuActive;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    public void OnOpenMenu(InputValue value)
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

    // Handle opening the debug console since unity hates 2 different player inputs both sending msgs
    public void OnOpenDebugConsole(InputValue value)
    {
        if (DebugController.Instance != null)
            DebugController.Instance.ToggleConsole();
    }

    public void OnReturn(InputValue value)
    {
        if (DebugController.Instance != null)
            DebugController.Instance.HandleReturn();
    }

}
