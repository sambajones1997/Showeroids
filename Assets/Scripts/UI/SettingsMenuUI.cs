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
    public void OnMasterVolumeSoundChanged(Slider volumeSlider)
    {
        mAudioMixer.SetFloat("MasterVol", volumeSlider.value);
    }

    public void OnMusicVolumeSoundChanged(Slider volumeSlider)
    {
        mAudioMixer.SetFloat("MusicVol", volumeSlider.value);
    }

    public void OnSFXVolumeSoundChanged(Slider volumeSlider)
    {
        mAudioMixer.SetFloat("SFXVol", volumeSlider.value);
    }

    public void OnCloseSettingsMenuPressed()
    {
        mSettingsMenu.SetActive(false);
    }

    public void OnResetLeaderboardPressed()
    {
        mResetLeadboardPopup.SetActive(true);
        mSettingsMenu.SetActive(false);
    }
}
