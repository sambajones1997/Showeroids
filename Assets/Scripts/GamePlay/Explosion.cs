using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AsteroidPoolingManager PoolingManager;

    public void OnParticleSystemStopped()
    {
        if (!PoolingManager)
        {
            PoolingManager = AsteroidPoolingManager.GetPoolingManager();
        }

        if (PoolingManager)
        {
            PoolingManager.ReleaseExplosionToPool(gameObject);
        }
    }
}
