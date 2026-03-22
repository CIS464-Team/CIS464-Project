using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private GameObject menuCanvas;

    public void StartSession()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Session, SceneDatabase.Scenes.Session)
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Tutorial, setActive:true)
            .Unload(SceneDatabase.Slots.Menu)
            .WithOverlay()
            .WithClearUnusedAssets()
            .Perform();
    }
    public void OpenSettings()
    {
        Debug.Log("Trying to open settings...");

        // pull the first item with a menu canvas tag as the game object
        menuCanvas = GameObject.FindGameObjectWithTag("MenuCanvas");

        Debug.Log(menuCanvas);

        // activate the menu canvas
        menuCanvas.SetActive(true);

        // old stuff
        // SceneController.Instance
        //     .NewTransition()
        //     .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.SettingsWindow, setActive:true)
        //     .WithOverlay()
        //     .Perform();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
