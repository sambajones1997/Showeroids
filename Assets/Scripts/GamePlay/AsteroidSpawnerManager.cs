using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnerPair
{
    public GameObject SpawnerObject;
    public int MinDifficultyLevel;
}

public class AsteroidSpawnerManager : MonoBehaviour
{
    [SerializeField] List<SpawnerPair> m_AsteroidSpawnerLocations;

    void Start()
    {
        int CurrentIndex = GameData.GetLevelDifficulty();
        SpawnerPair AsteroidSpawnerToUse = new SpawnerPair();

        foreach (SpawnerPair Pair in m_AsteroidSpawnerLocations)
        {
            if (Pair.MinDifficultyLevel <= CurrentIndex)
            {
                AsteroidSpawnerToUse = Pair;
            }
        }

        if (AsteroidSpawnerToUse.SpawnerObject)
        {
            AsteroidSpawnerToUse.SpawnerObject.SetActive(true);
        }
    }
}
