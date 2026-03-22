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
        menuCanvas.SetActive(true);
        // pull the first item with a menu canvas tag as the game object
        menuCanvas = GameObject.Find("Menu");

        // Debug.Log(menuCanvas);

        // activate the menu canvas
        // menuCanvas.SetActive(true);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
