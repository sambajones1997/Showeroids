using UnityEngine;

public static class AudioManager
{
    static float MaxVoldb = 0.0f;
    static float MinVoldb = -80.0f;

    static string MasterVolName = "MasterVol";
    static string MusicVolName = "MusicVol";
    static string SFXVolName = "SFXVol";

    static float MasterVol = 1.0f;
    static float MusicVol = 1.0f;
    static float SFXVol = 1.0f;

    public static float GetVolAtPercentage(float percentage)
    {
        return Mathf.Lerp(MinVoldb, MaxVoldb, percentage);
    }

    public static void LoadUserSettings()
    {
        // Master Vol Saving
        MasterVol = PlayerPrefs.GetFloat(MasterVolName);
        MusicVol = PlayerPrefs.GetFloat(MusicVolName);
        SFXVol = PlayerPrefs.GetFloat(SFXVolName);
    }

    public static void SaveUserSettings()
    {
        PlayerPrefs.SetFloat(MasterVolName, MasterVol);
        PlayerPrefs.SetFloat(MusicVolName, MusicVol);
        PlayerPrefs.SetFloat(SFXVolName, SFXVol);

        PlayerPrefs.Save();
    }

    public static void SetMasterVol(float vol)
    {
        MasterVol = vol;
    }

    public static void SetMusicVol(float vol)
    {
        MusicVol = vol;
    }

    public static void SetSFXVol(float vol)
    {
        SFXVol = vol;
    }

    public static float GetRawMasterVol() { return MasterVol; }
    public static float GetRawMusicVol() { return MusicVol; }
    public static float GetRawSFXVol() { return SFXVol; }

    public static float GetMasterVol() { return GetVolAtPercentage(MasterVol); }
    public static float GetMusicVol() { return GetVolAtPercentage(MusicVol); }
    public static float GetSFXVol() { return GetVolAtPercentage(SFXVol); }

    public static string GetMasterVolName() { return MasterVolName; }
    public static string GetMusicVolName() { return MusicVolName; }
    public static string GetSFXVolName() { return SFXVolName; }
}
