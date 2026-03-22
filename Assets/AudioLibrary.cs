using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    [SerializeField] private SFXgroup[] SFXgroups;
    private Dictionary<string, List<AudioClip>> soundDictionary;

    //create dictionary to store all clips in one group
    private void Awake()
    {
        InitializeDictionary();
    }

    //loop through each sound group set the value to an array of audio clips
    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach(SFXgroup SFXgroup in SFXgroups)
        {
            soundDictionary[SFXgroup.name] = SFXgroup.audioClips;
        }
    }

    //return a random clip from the list of clips
    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if (audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
            }
        }
        return null;
    }
}

    //So that it shows up on the unity instector
    [System.Serializable]
        public struct SFXgroup
    {
        public string name;
        public List<AudioClip> audioClips;
    }
