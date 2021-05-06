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
    public AudioSourceAndIdentifier[] music;
    public float nextSongDelay;

    Dictionary<string, AudioSource> audioDictionary = new Dictionary<string, AudioSource>();

    bool isMusicPlaying = false;
    bool isMusicPaused = false;
    int _musicPosition;

    public int MusicPosition
    {
        get { return _musicPosition % music.Length; }
    }

    void incrementMusicPosition() { ++_musicPosition; }

    void Start()
    {
        loadAudioSources();
        if (music.Length != 0) { _musicPosition = 0; }
    }

    void Update()
    {
        // if music is still playing but has finished current song, increment and start next
        if (isMusicPlaying && !music[MusicPosition].audio.isPlaying)
        {
            incrementMusicPosition();
            music[MusicPosition].audio.PlayDelayed(nextSongDelay);
        }
    }

    public void startMusic()
    {
        isMusicPlaying = true;
        if (isMusicPaused)
        {
            music[MusicPosition].audio.UnPause();
        }
        else
        {
            music[MusicPosition].audio.Play();
        }
        isMusicPaused = false;
    }

    public void stopMusic()
    {
        isMusicPlaying = false;
        music[MusicPosition].audio.Pause();
        isMusicPaused = true;
    }

    public void nextSong()
    {
        music[MusicPosition].audio.Stop();
        incrementMusicPosition();
        music[MusicPosition].audio.Play();
        isMusicPlaying = true;
        isMusicPaused = false;
    }

    public bool isGameMusicPlaying() { return isMusicPlaying; }

    void loadAudioSources()
    {
        for (int i = 0; i < audioSources.Length; ++i)
        {
            audioSources[i].audio.volume = audioSources[i].volume;
            audioDictionary.Add(audioSources[i].identifier, audioSources[i].audio);
        }
        for (int i = 0; i < music.Length; ++i)
        {
            music[i].audio.volume = music[i].volume;
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
