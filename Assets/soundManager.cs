using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [Header("--------------Audio Source----------")]
    private static AudioSource audioSource;
    private static AudioLibrary audioLibrary;
    public static soundManager Instance;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            audioLibrary = GetComponent<AudioLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }  

    public void PlaySFX(string soundName)
    {
        if(PauseManager.IsGamePaused)
        {
            return;
        }
        AudioClip audioClip = audioLibrary.GetRandomClip(soundName);
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }


}
