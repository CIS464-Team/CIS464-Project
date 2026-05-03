using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineControl : MonoBehaviour
{
    // PlayableDirector component reference to control the timeline
    [SerializeField] private PlayableDirector director;
    [SerializeField] public int cutsceneID;
    private LanternPickup lanternPickup;
    private GameObject player;
    private bool[] skipped = new bool[2] {false, false}; // make this as big as however many cutscenes you have!
    private TutorialText tutorialText;
    [SerializeField] public GameObject skipText;

    void Start()
    {
        // find lantern controller
        lanternPickup = GameObject.FindFirstObjectByType<LanternPickup>();

        // find player object
        player = GameObject.FindGameObjectWithTag("Player");

        // find tutorial text controller
        tutorialText = GameObject.FindFirstObjectByType<TutorialText>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !skipped[cutsceneID])
        {
            skipped[cutsceneID] = true;
            SkipCutscene();
        }
    }

    public void EndGame()
    {
        // send to end title scene (skipping loading screen)
        SceneController.Instance
                .NewTransition()
                .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.EndTitle, setActive:true)
                .Perform();
    }

    public void SkipCutscene()
    {

        // Jump to the end of the timeline
        director.time = director.duration;

        // Force the timeline to evaluate at the new time instantly
        director.Evaluate(); 

        // Optionally stop the director if it doesn't stop automatically
        director.Stop();

        // pickup the lantern
        lanternPickup.PickupLantern(player);

        // remove the skip text
        skipText.SetActive(false);

        // disable the tutorial script
        tutorialText.cutsceneSkipped = true;
    }

    public void PauseTimeline()
    {
        director.Pause();
    }

    public void ResumeTimeline()
    {
        director.Resume();
    }
}