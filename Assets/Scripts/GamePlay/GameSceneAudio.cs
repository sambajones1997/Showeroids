using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAudio : MonoBehaviour
{
    [SerializeField] AudioSource ImpactAudioSource;
    [SerializeField] AudioSource TreasureAudioSource;
    [SerializeField] AudioSource ExplosionAudioSource;

    [SerializeField] List<AudioClip> ImpactAudioClips;
    [SerializeField] List<AudioClip> TreasureCollectedAudioClips;
    [SerializeField] List<AudioClip> ExplosionAudioClips;

    static GameSceneAudio Instance;

    public void Start()
    {
        Instance = this;
    }

    public static GameSceneAudio GetGameSceneAudioInstance()
    {
        return Instance;
    }

    public void PlayImpactSFX()
    {
        if (ImpactAudioSource)
        {
            ImpactAudioSource.clip = ImpactAudioClips[Random.Range(0, ImpactAudioClips.Count - 1)];
            ImpactAudioSource.Play();
        }
    }

    public void PlayTreasureSFX()
    {
        if (TreasureAudioSource)
        {
            TreasureAudioSource.clip = TreasureCollectedAudioClips[Random.Range(0, TreasureCollectedAudioClips.Count - 1)];
            TreasureAudioSource.Play();
        }
    }

    public void PlayExplosionSFX()
    {
        if (ExplosionAudioSource)
        {
            ExplosionAudioSource.clip = ExplosionAudioClips[Random.Range(0, ExplosionAudioClips.Count - 1)];
            ExplosionAudioSource.Play();
        }
    }
}
