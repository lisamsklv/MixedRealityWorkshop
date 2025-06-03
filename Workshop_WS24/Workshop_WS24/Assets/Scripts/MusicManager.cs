using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> musicTracks; // Liste deiner Musikstücke
    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayCurrentTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    void PlayCurrentTrack()
    {
        if (musicTracks.Count == 0) return;

        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();
    }

    void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
        PlayCurrentTrack();
    }
}