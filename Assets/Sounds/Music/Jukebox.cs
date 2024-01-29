using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Jukebox : MonoBehaviour
{
    AudioSource mySource;

    public AudioClip[] tracks;

    void Start()
    {
        mySource = GetComponent<AudioSource>();

        NextTrack();
    }

    void Update()
    {
        if (!mySource.isPlaying)
        {
            NextTrack();
        }
    }

    void NextTrack()
    {
        mySource.clip =  tracks[Random.Range(0, tracks.Length)];
        mySource.Play();
    }
}
