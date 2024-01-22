using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] AudioMixer mAudioMixer;
    [SerializeField] GameObject mSettingsMenu;
    [SerializeField] GameObject mResetLeadboardPopup;

    [Header("Audio Sliders")]
    [SerializeField] Slider mMasterVolSlider;
    [SerializeField] Slider mMusicVolSlider;
    [SerializeField] Slider mSFXVolSlider;

    public void Start()
    {
        AudioManager.LoadUserSettings();

        // Audio Slider's Prepping

        if (mMasterVolSlider)
        {
            mMasterVolSlider.value = AudioManager.GetRawMasterVol();
        }

        if (mMusicVolSlider)
        {
            mMusicVolSlider.value = AudioManager.GetRawMusicVol();
        }
        
        if (mSFXVolSlider)
        {
            mSFXVolSlider.value = AudioManager.GetRawSFXVol();
        }

        // Audio Mixer Group Loading

        if (mAudioMixer)
        {
            mAudioMixer.SetFloat(AudioManager.GetMasterVolName(), AudioManager.GetMasterVol());
            mAudioMixer.SetFloat(AudioManager.GetMusicVolName(), AudioManager.GetMusicVol());
            mAudioMixer.SetFloat(AudioManager.GetSFXVolName(), AudioManager.GetSFXVol());
        }
    }

    public void OnMasterVolumeSoundChanged()
    {
        AudioManager.SetMasterVol(mMasterVolSlider.value);
        mAudioMixer.SetFloat(AudioManager.GetMasterVolName(), AudioManager.GetMasterVol());
    }
     
    public void OnMusicVolumeSoundChanged()
    {
        AudioManager.SetMusicVol(mMusicVolSlider.value);
        mAudioMixer.SetFloat(AudioManager.GetMusicVolName(), AudioManager.GetMusicVol());
    }

    public void OnSFXVolumeSoundChanged()
    {
        AudioManager.SetSFXVol(mSFXVolSlider.value);
        mAudioMixer.SetFloat(AudioManager.GetSFXVolName(), AudioManager.GetSFXVol());
    }

    public void OnCloseSettingsMenuPressed()
    {
        SaveUserSettings();
        mSettingsMenu.SetActive(false);
    }

    public void OnResetLeaderboardPressed()
    {
        mResetLeadboardPopup.SetActive(true);
        mSettingsMenu.SetActive(false);
    }

    private void SaveUserSettings()
    {
        AudioManager.SaveUserSettings();
    }
}
