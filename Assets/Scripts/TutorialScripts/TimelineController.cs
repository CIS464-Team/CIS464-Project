using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    // PlayableDirector component reference to control the timeline
    [SerializeField] private PlayableDirector director;

    public void PauseTimeline()
    {
        director.Pause();
    }

    public void ResumeTimeline()
    {
        director.Resume();
    }
}