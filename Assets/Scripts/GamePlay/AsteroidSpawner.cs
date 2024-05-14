using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint
{
    public SpawnPoint(GameObject gameObject, float timer)
    {
        SpawnPointLocation = gameObject;
        SpawnTimer = timer;
    }

    public GameObject SpawnPointLocation;
    public float SpawnTimer;
}

public class AsteroidSpawner : MonoBehaviour
{
    List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();
    List<AsteroidSpawnPoint> m_AsteroidSpawnPoints = new List<AsteroidSpawnPoint>();
    List<int> SpawnIndexes = new List<int>();
    AsteroidPoolingManager PoolManager;
    [SerializeField] GameSceneManager mGameSceneManager;
    float Cooldown = 0.0f;

    private void Start()
    {
        int index = 0;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
            {
                SpawnPoints.Add(new SpawnPoint(t.gameObject, 0.0f));
                m_AsteroidSpawnPoints.Add(t.gameObject.GetComponent<AsteroidSpawnPoint>());
                SpawnIndexes.Add(index);
                index++;
            }
        } 
    }

    private void FixedUpdate()
    {
        if (!mGameSceneManager)
        {
            return;
        }

        if (mGameSceneManager.GetLevelGameState() == GameSceneManager.EGameState.Outro ||
                mGameSceneManager.GetLevelGameState() == GameSceneManager.EGameState.Ended)
        {
            this.enabled = false;
        }

        if (mGameSceneManager.GetLevelGameState() == GameSceneManager.EGameState.Playing)
        {
            foreach (SpawnPoint sp in SpawnPoints)
            {
                sp.SpawnTimer -= Time.fixedDeltaTime;
            }

            Cooldown -= Time.fixedDeltaTime;

            if (Cooldown <= 0.0f)
            {
                SpawnAsteroid();
                Cooldown = 0.5f;
            }
        }
    }

    private void SpawnAsteroid()
    {
        PoolManager = AsteroidPoolingManager.GetPoolingManager();

        if (!PoolManager)
        {
            return;
        }

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            int index = SpawnIndexes[i];
            if (SpawnPoints[index].SpawnTimer <= 0.0f)
            {
                GameObject obj = PoolManager.GetPooledAsteroid();
                obj.SetActive(true);

                Vector3 Pos = SpawnPoints[index].SpawnPointLocation.transform.position;
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));
                obj.transform.SetPositionAndRotation(Pos, transform.rotation);
                obj.GetComponentInChildren<Renderer>().transform.SetLocalPositionAndRotation(Vector3.zero, spawnRotation);

                Asteroid ast = obj.GetComponent<Asteroid>();

                if (ast)
                {
                    ast.StartPooledObject(m_AsteroidSpawnPoints[index].minX, m_AsteroidSpawnPoints[index].maxX);
                }

                SpawnPoints[index].SpawnTimer = 2.0f;
                break;
            }
        }

        ShuffleArrays();
    }

    void ShuffleArrays()
    {
        for (int i = 0; i < SpawnIndexes.Count; i++)
        {
            int r = UnityEngine.Random.Range(i, SpawnIndexes.Count);
            int temp = SpawnIndexes[i];

            SpawnIndexes[i] = SpawnIndexes[r];
            SpawnIndexes[r] = temp;
        }
    }
           
}
