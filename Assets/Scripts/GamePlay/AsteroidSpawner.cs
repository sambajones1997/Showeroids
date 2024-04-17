using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    List<GameObject> SpawnPoints = new List<GameObject>();
    AsteroidPoolingManager PoolManager;
    float Cooldown = 2.0f;

    private void Start()
    {
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
            {
                SpawnPoints.Add(t.gameObject);
            }
        } 
    }

    private void FixedUpdate()
    {
        Cooldown -= Time.fixedDeltaTime;

        if (Cooldown <= 0.0f)
        {
            Cooldown = 2.0f;

            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        PoolManager = AsteroidPoolingManager.GetPoolingManager();

        if (PoolManager)
        {
            GameObject obj = PoolManager.GetPooledAsteroid();
            obj.SetActive(true);

            Vector3 Pos = SpawnPoints[Random.Range(0,SpawnPoints.Count - 1)].transform.position;
            obj.transform.SetPositionAndRotation(Pos, transform.rotation);

            Asteroid ast = obj.GetComponent<Asteroid>();

            if (ast)
            {
                ast.StartPooledObject();
            }
        }
    }
}
