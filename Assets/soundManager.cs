using Unity.VisualScripting;
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
        AudioClip audioClip = audioLibrary.GetRandomClip(soundName);
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

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
