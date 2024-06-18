using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public static class SettingsStrings
{
    public static string PlayerPrefsInputType = "PlayerInputType";
    public static string PlayerLives = "PlayerLives";
}

public class SettingsMenuUI : MonoBehaviour
{
    [SerializeField] AudioMixer mAudioMixer;
    [SerializeField] GameObject mSettingsMenu;
    [SerializeField] GameObject mSettingsMenuCanvas;
    [SerializeField] GameObject mResetLeadboardPopup;

    [Header("DropDown")]
    [SerializeField] Dropdown mDropDown;

    [Header("Audio Sliders")]
    [SerializeField] Slider mMasterVolSlider;
    [SerializeField] Slider mMusicVolSlider;
    [SerializeField] Slider mSFXVolSlider;

    [Header("Saved")]
    [SerializeField] TMPro.TextMeshProUGUI mSavedText;

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Start()
    {
        AudioManager.LoadUserSettings();
        mSavedText.enabled = false;

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

        if (mDropDown)
        {
            mDropDown.value = PlayerPrefs.GetInt(SettingsStrings.PlayerPrefsInputType, 0);
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
        mSettingsMenu.SetActive(true); // Ensure we're open for the next time
        mSettingsMenuCanvas.SetActive(false);
    }

    public void OnSaveSettingsPressed()
    {
        SaveUserSettings();
        if (mSavedText)
        {
            mSavedText.enabled = true;
            StopAllCoroutines(); // Ensure we don't have more than one coroutine going
            StartCoroutine("TurnOffSavedText");
        }
    }

    public void OnResetLeaderboardPressed()
    {
        mResetLeadboardPopup.SetActive(true);
        mSettingsMenu.SetActive(false);
        ShoweroidsFileSystem.ResetLeaderBoard();
    }

    private void SaveUserSettings()
    {
        AudioManager.SaveUserSettings();
        if (mDropDown)
        {
            PlayerPrefs.SetInt(SettingsStrings.PlayerPrefsInputType, mDropDown.value);
            PlayerPrefs.Save();
        }
    }

    IEnumerator TurnOffSavedText()
    {
        yield return new WaitForSeconds(2.0f);

        if (mSavedText)
        {
            mSavedText.enabled = false;
        }
        yield break;
    }
}
