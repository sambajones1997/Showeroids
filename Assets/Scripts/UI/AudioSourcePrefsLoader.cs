using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePrefsLoader : MonoBehaviour
{
    [SerializeField] AudioMixer mAudioMixer;

    // Start is called before the first frame update
    void Start()
    {
        if (mAudioMixer)
        {
            // Master Vol Loading
            mAudioMixer.SetFloat(AudioManager.GetMasterVolName(), AudioManager.GetMasterVol());

            // Music Vol Loading
            mAudioMixer.SetFloat(AudioManager.GetMusicVolName(), AudioManager.GetMusicVol());

            // SFX Vol Loading
            mAudioMixer.SetFloat(AudioManager.GetSFXVolName(), AudioManager.GetSFXVol());
        }

        Destroy(this);
    }
}
