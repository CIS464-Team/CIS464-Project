using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SaveManager : MonoBehaviour
{
    private string saveLocation;
    private InGameMenuManager menuManager;
    private KeyManager keyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set up save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        // get the menu manager
        menuManager = FindFirstObjectByType<InGameMenuManager>();

        // get the key manager
        keyManager = FindFirstObjectByType<KeyManager>();
    }

    public void SaveGame()
    {
        string loadedScene = GetActiveScene();

        // only allow save if we are in gameplay
        if (loadedScene != "MainMenu")
        {
            // create save structure
            SaveData saveData = new SaveData
            {
                currentArea = loadedScene,
                keysHeld = keyManager.GetKeysHeld()
            };

            // save to file
            Debug.Log($"File saved at {saveLocation}");

            File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        }
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            // load save from file
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            // apply new scene transition
            SceneController.Instance
                .NewTransition()
                .Load(SceneDatabase.Slots.SessionContent, saveData.currentArea, setActive:true)
                .Unload(SceneDatabase.Slots.Menu)
                .WithOverlay()
                .WithClearUnusedAssets()
                .Perform();
            menuManager.isMainMenuActive = false;
            menuManager.CloseSettings();
        }
        else
        {
            // set up initial save file (if possible)
            SaveGame();
        }
    }

    private string GetActiveScene()
    {
        int numScenes = SceneManager.sceneCount; // get the scene count

        // iterate through all scenes
        for (int i = 0; i < numScenes; i++)
        {
            Scene nextScene = SceneManager.GetSceneAt(i); // get the scene at this index

            Debug.Log($"Save: Checking for valid scene {nextScene.name}");

            // check if this scene isn't persistent (core, session)
            if (nextScene.name != "Core" && nextScene.name != "Session")
            {
                Debug.Log($"Save: Found valid scene {nextScene.name}");
                return nextScene.name;
            }
        }

        // default to main menu
        return "MainMenu";
    }
}
