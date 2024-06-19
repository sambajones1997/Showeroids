using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    AsteroidPoolingManager PoolingManager;
    [SerializeField] GameSceneManager mGameSceneManager;
    [SerializeField] PlayerController mPlayerController;
    [SerializeField] float mMinSpawnX;
    [SerializeField] float mMaxSpawnX;

    float OffsetTimer = 5.0f;
    float SpawnTimer = 15.0f;
    List<float> SpawnTimers = new List<float>();
    bool bSpawningEnabled = false;
    int NumTreasuresToSpawn = 0;

    public void FixedUpdate()
    {
        if (!bSpawningEnabled)
        {
            return;
        }

        SpawnTimer -= Time.fixedDeltaTime;
        if (SpawnTimer <= 0.0f)
        {
            SpawnTreasure();
        }
    }

    public void PlayerDead()
    {
        if (mPlayerController)
        {
            mPlayerController.PlayerDiedEvent.RemoveListener(PlayerDead);
        }
        EnableSpawning(false);
    }

    public void EnableSpawning(bool enabled)
    {
        bSpawningEnabled = enabled;
        if (mGameSceneManager)
        {
            NumTreasuresToSpawn = GameData.GetLevelDifficulty() + 1;
            float maxTimer = (mGameSceneManager.mLevelTimer - OffsetTimer) / NumTreasuresToSpawn;

            for (int i = 0; i < NumTreasuresToSpawn; i++)
            {
                SpawnTimers.Add(Random.Range(OffsetTimer * 2.0f, maxTimer));
            }

            GetNextSpawnTimer();
        }

        if (mPlayerController)
        {
            mPlayerController.PlayerDiedEvent.AddListener(PlayerDead);
        }
    }

    public void SpawnTreasure()
    {
        if (!PoolingManager)
        {
            PoolingManager = AsteroidPoolingManager.GetPoolingManager();
        }

        if (PoolingManager)
        {
            GameObject TreasureObject = PoolingManager.GetPooledTreasure();
   
            if (TreasureObject)
            {
                Vector3 SpawnPos = new Vector3(Random.Range(mMinSpawnX, mMaxSpawnX), transform.position.y, transform.position.z);
                TreasureObject.transform.parent = null;
                TreasureObject.transform.SetPositionAndRotation(SpawnPos, Quaternion.identity);
                TreasureObject.SetActive(true);
            }
        }

        GetNextSpawnTimer();
    }

    private void GetNextSpawnTimer()
    {
        if (SpawnTimers.Count > 0)
        {
            SpawnTimer = SpawnTimers[0];
            SpawnTimers.RemoveAt(0);
        }
        else
        {
            bSpawningEnabled = false;
            this.enabled = false;
        }
    }
}
