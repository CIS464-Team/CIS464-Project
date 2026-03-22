using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private static MusicManager Instance;
    // We manage Music and Ambiance separately so we can Pause each independently and change volume independently
    private AudioSource MusicSource;
    private AudioSource AmbianceSource;
    public AudioClip bgMusic;
    public AudioClip Ambiance;
    // 2 independent sliders to control Music and Ambiance seperately
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambianceSlider;
   

    private void Awake()
    {
        //If MusicManager is empty get 2 Audiosource Components for Music and ambiance
        if (Instance == null)
        {
            Instance = this;
            AudioSource[] sources = GetComponents<AudioSource>();
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
        //if audio is already playing do not restart the song
        if (bgMusic != null) PlayBackgroundMusic(false, bgMusic);
        if (Ambiance != null) PlayAmbiance(false, Ambiance);

        //Add listeners to check for a change of value and set the volume to that value
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
        //set music to selected audio clip if it is not null and play it
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

        //set ambiance to slecrted audio clip if it is not null and play it
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
