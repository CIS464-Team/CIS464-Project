using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private GameObject menuCanvas;
    private InGameMenuManager menuManager;
    private SaveManager saveManager;

    void Start()
    {
        menuManager = FindFirstObjectByType<InGameMenuManager>();
        menuManager.isMainMenuActive = true;

        saveManager = FindFirstObjectByType<SaveManager>();
    }

    public void StartSession()
    {
        saveManager.NewSaveFile();
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Session, SceneDatabase.Scenes.Session)
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Tutorial, setActive:true)
            .Unload(SceneDatabase.Slots.Menu)
            .WithOverlay()
            .WithClearUnusedAssets()
            .Perform();
        menuManager.isMainMenuActive = false;
    }
    public void OpenSettingsButton()
    {
        menuManager.OpenSettings();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
