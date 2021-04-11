using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioSourceAndIdentifier
    {
        public AudioSource audio;
        public string identifier;
        [Range(0.0f, 1.0f)]
        public float volume;
    }

    public AudioSourceAndIdentifier[] audioSources;
    Dictionary<string, AudioSource> audioDictionary = new Dictionary<string, AudioSource>();

    private void Start()
    {
        loadAudioSources();
    }

    private void loadAudioSources()
    {
        for (int i = 0; i < audioSources.Length; ++i)
        {
            audioSources[i].audio.volume = audioSources[i].volume;
            audioDictionary.Add(audioSources[i].identifier, audioSources[i].audio);
        }
    }

    public void playSound(string sound)
    {
        audioDictionary[sound].Play();
    }

    public void stopSound(string sound)
    {
        audioDictionary[sound].Stop();
    }
}
