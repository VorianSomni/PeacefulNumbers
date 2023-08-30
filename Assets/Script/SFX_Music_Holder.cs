using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Music_Holder : MonoBehaviour
{
    private byte whichMusicIsPlayingNow;
    bool firstTime = true;

    // SFX audio clips
    public AudioClip[] SFX_clips;
    // END SFX audio clips

    // Music audio clips
    public AudioClip[] Music;
    // END Music audio clips

    private void Start()
    {
        whichMusicIsPlayingNow = 0;
    }

    public AudioClip GetAudioClip(int audio)
    {
        return SFX_clips[audio];
    }

    public AudioClip GetMusic()
    {
        AudioClip audioClipToBePlayed;

        if (firstTime)
        {
            firstTime = false;
            return Music[whichMusicIsPlayingNow];
        }
        
        while (true)
        {
            byte music = (byte)Random.Range(0, Music.Length);
            if (music != whichMusicIsPlayingNow)
            {
                string name = Music[music].name;
                audioClipToBePlayed = Resources.Load<AudioClip>("Music/" + name);
                break;
            }
        }
        return audioClipToBePlayed;
    }


}
