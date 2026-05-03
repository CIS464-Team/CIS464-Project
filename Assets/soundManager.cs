using UnityEngine;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [Header("--------------Audio Source----------")]
    private static AudioSource audioSource;
    private static AudioLibrary audioLibrary;
    public static soundManager Instance;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        //If sound manager is empty Get audio source and Audio library components
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
        Debug.Log("PlaySFX called: " + soundName);
        if(PauseManager.IsGamePaused)
        {
            return;
        }
        AudioClip audioClip = audioLibrary.GetRandomClip(soundName);
        Debug.Log("Clip found: " + audioClip);
        if (audioClip != null)
        {
            Debug.Log("Playing: " + audioClip);
            audioSource.PlayOneShot(audioClip);
        }
    }

        //listen for change in volume set the new volume
    void Start()
    {
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
    }


}
