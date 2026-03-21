using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip Ambiance;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
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
            PlayBackgroundMusic(false, Ambiance);
        }
        
    }
    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        } 
        if (audioSource.clip != null)
        {
            if (resetSong)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }




}
