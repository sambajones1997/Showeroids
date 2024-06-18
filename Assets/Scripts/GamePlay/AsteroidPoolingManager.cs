using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolingManager : MonoBehaviour
{
    public int PooledAsteroids = 0;
    public int NumActiveAsteroids = 0;
    public int NumActiveTreasures = 0;
    List<GameObject> Asteroids = new List<GameObject>();
    List<GameObject> Explosions = new List<GameObject>();
    List<GameObject> Treasures = new List<GameObject>();
    List<GameObject> TreasureVFX = new List<GameObject>();

    [SerializeField] List<GameObject> AsteroidsToSpawn = new List<GameObject>();
    [SerializeField] List<GameObject> TreasureToSpawn = new List<GameObject>();
    [SerializeField] GameObject ExplosionVFXToSpawn;
    [SerializeField] GameObject TreasureCollectedVFXToSpawn;

    static AsteroidPoolingManager Instance;

    public void Start()
    {
        Instance = this;
    }

    public GameObject GetPooledAsteroid()
    {
        GameObject asteroid = null;

        NumActiveAsteroids++;
        if (Asteroids.Count > 0)
        {
            asteroid = Asteroids[0];
            Asteroids.RemoveAt(0);
            PooledAsteroids--;
            return asteroid;
        }

        int Rand = Random.Range(0, AsteroidsToSpawn.Count);
        asteroid = Instantiate(AsteroidsToSpawn[Rand]);
        return asteroid;
    }

    public GameObject GetPooledTreasure()
    {
        GameObject treasure = null;

        NumActiveTreasures++;
        if (Treasures.Count > 0)
        {
            treasure = Treasures[0];
            Asteroids.RemoveAt(0);
            PooledAsteroids--;
            return treasure;
        }

        int Rand = Random.Range(0, TreasureToSpawn.Count);
        treasure = Instantiate(TreasureToSpawn[Rand]);
        return treasure;
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

    public GameObject GetPooledTreasureVFX()
    {
        GameObject treasureVFX = null;
        if (TreasureVFX.Count > 0)
        {
            treasureVFX = TreasureVFX[0];
            TreasureVFX.RemoveAt(0);
            return treasureVFX;
        }

        treasureVFX = Instantiate(TreasureCollectedVFXToSpawn);
        return treasureVFX;
    }

    public void ReleaseAsteroidToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Asteroids.Add(obj);
        PooledAsteroids = Asteroids.Count;
        NumActiveAsteroids--;
    }

    public void ReleaseExplosionToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Explosions.Add(obj);
    }

    public void ReleaseTreasureVFXToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        TreasureVFX.Add(obj);
    }

    public void ReleaseTreasureToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = gameObject.transform;
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        Treasures.Add(obj);
        NumActiveTreasures--;
    }

    public static AsteroidPoolingManager GetPoolingManager()
    {
        return Instance;
    }

    public int GetNumActiveAsteroids()
    {
        return NumActiveAsteroids;
    }
}
