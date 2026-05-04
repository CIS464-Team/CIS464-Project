using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineControl : MonoBehaviour
{
    // PlayableDirector component reference to control the timeline
    [SerializeField] private PlayableDirector director;
    private GameObject player;
    private TutorialText tutorialText;
    [SerializeField] public GameObject skipText;

    void Start()
    {
        // find player object
        player = GameObject.FindGameObjectWithTag("Player");

        // find tutorial text controller
        tutorialText = GameObject.FindFirstObjectByType<TutorialText>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        // Jump to the end of the timeline
        director.time = director.duration;

        // Force the timeline to evaluate at the new time instantly
        director.Evaluate(); 

        // Optionally stop the director if it doesn't stop automatically
        director.Stop();

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