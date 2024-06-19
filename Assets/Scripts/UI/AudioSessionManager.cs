using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSessionManager : MonoBehaviour
{
    [SerializeField] AudioMixer mAudioMixer;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.LoadUserSettings();

        // Audio Mixer Group Loading
        if (mAudioMixer)
        {
            mAudioMixer.SetFloat(AudioManager.GetMasterVolName(), AudioManager.GetMasterVol());
            mAudioMixer.SetFloat(AudioManager.GetMusicVolName(), AudioManager.GetMusicVol());
            mAudioMixer.SetFloat(AudioManager.GetSFXVolName(), AudioManager.GetSFXVol());
        }
    }
}
