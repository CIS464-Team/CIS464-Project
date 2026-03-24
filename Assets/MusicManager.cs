using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private static MusicManager Instance;
    // We manage Music and Ambiance separately so we can Pause each independently and change volume independently
    private AudioSource MusicSource;
    private AudioSource AmbianceSource;
    public AudioClip bgMusic;
    public AudioClip Ambiance;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambianceSlider;

    private void Awake()
    {
        // If the MusicManager is empty, get 2 AudioSource components and link to the right AudioSource
        if (Instance == null)
        {
            Instance = this;
            var sources = GetComponents<AudioSource>();
            MusicSource = sources[0];
            AmbianceSource = sources[1];
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If audio is already playing, do not restart the song.
        if (bgMusic != null) PlayBackgroundMusic(false, bgMusic);
        if (Ambiance != null) PlayAmbiance(false, Ambiance);
        // Listen for a change in value and set the volume accordingly.
        musicSlider.onValueChanged.AddListener(val => MusicSource.volume = val); 
        ambianceSlider.onValueChanged.AddListener(val => AmbianceSource.volume = val); 
        
    }

    void Update()
    {
        // Every frame we check for whether or not the game is paused and pause music if needed.
        if (PauseManager.IsGamePaused)
        {
            MusicSource.Pause();
            AmbianceSource.Pause();
        }
        else
        {
            if (!MusicSource.isPlaying)
            {
                MusicSource.UnPause();
                AmbianceSource.UnPause();
            }
        }
    }

    // Set the desired audio clip and play it
    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            MusicSource.clip = audioClip;
        } 
        if (MusicSource.clip != null)
        {
            if (resetSong)
            {
                MusicSource.Stop();
            }
            MusicSource.Play();
        }
    }

    public void PlayAmbiance(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            AmbianceSource.clip = audioClip;
        } 
        if (AmbianceSource.clip != null)
        {
            if (resetSong)
            {
                AmbianceSource.Stop();
            }
            AmbianceSource.Play();
        }
    }
}
