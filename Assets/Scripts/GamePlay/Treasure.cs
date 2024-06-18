using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] Rigidbody RigidBody;
    [SerializeField] GameObject AsteroidSprite;  
    Vector3 MovementVector = -Vector3.up;
    AsteroidPoolingManager Manager;

    [SerializeField] float CurrentSpeed = 1.0f;
    [SerializeField] public int ScoreToGive = 2500;

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + (MovementVector * CurrentSpeed * Time.fixedDeltaTime));

        if (transform.position.y <= -6.0f)
        {
            DestroyTreasure();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerScore ps = collision.transform.root.GetComponentInChildren<PlayerScore>();
        if (ps)
        {
            ps.AddScore(ScoreToGive);
        }

        if (FindManager() && Manager)
        {
            GameObject vfx = Manager.GetPooledTreasureVFX();
            vfx.SetActive(true);
            vfx.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
        DestroyTreasure();
    }

    private void DestroyTreasure()
    {
        if (FindManager() && Manager)
        {
            Manager.ReleaseTreasureToPool(gameObject);
        }
    }

    private bool FindManager()
    {
        if (!Manager)
        {
            Manager = AsteroidPoolingManager.GetPoolingManager();
        }

        return Manager != null;
    }
}
