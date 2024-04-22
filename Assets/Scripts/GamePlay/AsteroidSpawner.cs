using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    AsteroidPoolingManager PoolManager;
    float Cooldown = 2.0f;

    private void Start()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
            {
                SpawnPoints.Add(new SpawnPoint(t.gameObject, 0.0f));
            }
        } 
    }

    private void FixedUpdate()
    {
        Cooldown -= Time.fixedDeltaTime;

        foreach (SpawnPoint sp in SpawnPoints)
        {
            sp.SpawnTimer -= Time.fixedDeltaTime;
        }

        if (Cooldown <= 0.0f)
        {
            SpawnAsteroid();
            Cooldown = 0.5f;
        }
    }

    private void SpawnAsteroid()
    {
        PoolManager = AsteroidPoolingManager.GetPoolingManager();

        if (!PoolManager)
        {
            return;
        }

        foreach (var Spawner in SpawnPoints)
        {
            if (Spawner.SpawnTimer <= 0.0f)
            {
                GameObject obj = PoolManager.GetPooledAsteroid();
                obj.SetActive(true);

                Vector3 Pos = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)].SpawnPointLocation.transform.position;
                Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                obj.transform.SetPositionAndRotation(Pos, transform.rotation);
                obj.GetComponentInChildren<Renderer>().transform.SetLocalPositionAndRotation(Vector3.zero, spawnRotation);

                Asteroid ast = obj.GetComponent<Asteroid>();

                if (ast)
                {
                    ast.StartPooledObject();
                }

                Spawner.SpawnTimer = 2.0f;
                break;
            }
        }
    }
}
