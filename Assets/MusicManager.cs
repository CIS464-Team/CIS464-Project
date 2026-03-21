using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private static MusicManager Instance;
    private AudioSource MusicSource;
    private AudioSource AmbianceSource;
    public AudioClip bgMusic;
    public AudioClip Ambiance;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambianceSlider;
   

    private void Awake()
    {
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
        if (bgMusic != null) PlayBackgroundMusic(false, bgMusic);
        if (Ambiance != null) PlayAmbiance(false, Ambiance);
        
        musicSlider.onValueChanged.AddListener(val => MusicSource.volume = val); 
        ambianceSlider.onValueChanged.AddListener(val => AmbianceSource.volume = val); 
        
    }
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
