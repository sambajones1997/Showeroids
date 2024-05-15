using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Rigidbody RigidBody;
    [SerializeField] GameObject AsteroidSprite;

    [SerializeField] Vector3 MovementVector = Vector3.zero;
    [Header("Speed")]
    [SerializeField] float MinSpeed = 1.0f;
    [SerializeField] float MaxSpeed = 1.0f;
    [Header("Rotation")]
    [SerializeField] float MinRotationSpeed = 15.0f;
    [SerializeField] float MaxRotationSpeed = 45.0f;

    AsteroidPoolingManager Manager;

    float CurrentSpeed = 0.0f;
    float DeltaRotation = 0.0f;

    public void StartPooledObject(float minX, float maxX)
    {
        MovementVector = new Vector3(Random.Range(minX, maxX), -1.0f, 0.0f).normalized;

        CurrentSpeed = Random.Range(MinSpeed, MaxSpeed);
        DeltaRotation = Random.Range(MinRotationSpeed, MaxRotationSpeed);
    }

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + (MovementVector * CurrentSpeed * Time.fixedDeltaTime));
        AsteroidSprite.transform.Rotate(Vector3.back * DeltaRotation * Time.fixedDeltaTime);

        if (transform.position.y <= -6.0f)
        {
            DestroyAsteroid();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        PlayerController pc = collision.transform.root.GetComponentInChildren<PlayerController>();
        if (pc)
        {
            pc.TakeHit();
        }

        if (FindManager() && Manager)
        {
            GameObject explosion = Manager.GetPooledExplosion();
            explosion.SetActive(true);
            explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
        DestroyAsteroid();
    }


    private void DestroyAsteroid()
    {
        if (FindManager() && Manager)
        {
            Manager.ReleaseAsteroidToPool(gameObject);
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
