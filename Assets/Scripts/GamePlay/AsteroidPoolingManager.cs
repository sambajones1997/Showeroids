using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AsteroidPoolingManager : MonoBehaviour
{
    public int PooledAsteroids = 0;
    List<GameObject> Asteroids = new List<GameObject>();
    List<GameObject> Explosions = new List<GameObject>();

    [SerializeField] List<GameObject> AsteroidsToSpawn = new List<GameObject>();
    [SerializeField] GameObject ExplosionVFXToSpawn;

    static AsteroidPoolingManager Instance;

    public void Start()
    {
        Instance = this;
    }

    public GameObject GetPooledAsteroid()
    {
        GameObject asteroid = null;
        if (Asteroids.Count > 0)
        {
            asteroid = Asteroids[0];
            Asteroids.RemoveAt(0);
            PooledAsteroids--;
            return asteroid;
        }

        asteroid = Instantiate(AsteroidsToSpawn[Random.Range(0, Asteroids.Count)]);
        return asteroid;
    }

    public GameObject GetPooledExplosion()
    {
        GameObject explosion = null;
        if (Explosions.Count > 0)
        {
            explosion = Explosions[0];
            Explosions.RemoveAt(0);
            return explosion;
        }

        explosion = Instantiate(ExplosionVFXToSpawn);
        return explosion;
    }

    public void ReleaseAsteroidToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Asteroids.Add(obj);
        PooledAsteroids = Asteroids.Count;
    }

    public void ReleaseExplosionToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Explosions.Add(obj);
    }

    public static AsteroidPoolingManager GetPoolingManager()
    {
        return Instance;
    }
}
