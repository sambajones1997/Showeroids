using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum EExplosionType { Explosion, Treasure };

    [SerializeField] EExplosionType ExplosionType;
    AsteroidPoolingManager PoolingManager;

    public void OnParticleSystemStopped()
    {
        if (!PoolingManager)
        {
            PoolingManager = AsteroidPoolingManager.GetPoolingManager();
        }

        if (PoolingManager)
        {
            if (ExplosionType == EExplosionType.Explosion)
            {
                PoolingManager.ReleaseExplosionToPool(gameObject);
            }
            else
            {
                PoolingManager.ReleaseTreasureVFXToPool(gameObject);
            }
        }
    }
}
