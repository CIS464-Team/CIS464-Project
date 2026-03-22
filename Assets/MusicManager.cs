using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private static MusicManager Instance;
    private AudioSource musicSource;
    private AudioSource ambianceSource;
    public AudioClip bgMusic;
    public AudioClip Ambiance;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            var sources = GetComponents<AudioSource>();
            musicSource = sources[0];
            ambianceSource = sources[1];
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
        if (bgMusic != null)
        {
            PlayBackgroundMusic(false, bgMusic);
            PlayAmbiance(false, Ambiance);
        }
        
    }

    void Update()
    {
        if (PauseManager.IsGamePaused)
        {
            musicSource.Pause();
            ambianceSource.Pause();
        }
        else
        {
            if (!musicSource.isPlaying)
            {
                musicSource.UnPause();
                ambianceSource.UnPause();
            }
        }
    }
    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            musicSource.clip = audioClip;
        } 
        if (musicSource.clip != null)
        {
            if (resetSong)
            {
                musicSource.Stop();
            }
            musicSource.Play();
        }
    }

    public void PlayAmbiance(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            ambianceSource.clip = audioClip;
        } 
        if (ambianceSource.clip != null)
        {
            if (resetSong)
            {
                ambianceSource.Stop();
            }
            ambianceSource.Play();
        }
    }




}
