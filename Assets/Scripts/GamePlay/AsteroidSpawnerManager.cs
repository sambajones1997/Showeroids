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

    public void SpawnAsteroid(GameObject Spawner)
    {
        AsteroidPoolingManager Manager = AsteroidPoolingManager.GetPoolingManager();

        if (Manager)
        {
            GameObject AsteroidObject = Manager.GetPooledAsteroid();
            if (AsteroidObject)
            {
                AsteroidObject.transform.parent = null;
                AsteroidObject.transform.SetPositionAndRotation(Spawner.transform.position, Quaternion.identity);
                AsteroidObject.SetActive(true);
            }
        }
    }
}
