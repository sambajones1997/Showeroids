using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Rigidbody RigidBody;
    [SerializeField] GameObject AsteroidSprite;
    [SerializeField] ParticleSystem ImpactVFX;
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

    private void Start()
    {
        MovementVector = new Vector3(Random.Range(-0.5f, 0.5f), -1.0f, 0.0f).normalized;

        CurrentSpeed = Random.Range(MinSpeed , MaxSpeed);
        DeltaRotation = Random.Range(MinRotationSpeed, MaxRotationSpeed);
    }

    private void OnEnable()
    {
        Manager = AsteroidPoolingManager.GetPoolingManager();
        if (Manager)
        {

        }
    }

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + (MovementVector * CurrentSpeed * Time.fixedDeltaTime));
        AsteroidSprite.transform.Rotate(Vector3.back * DeltaRotation * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerController pc = other.transform.root.GetComponentInChildren<PlayerController>();
        if (pc)
        {
            pc.TakeHit();
        }

        if (FindManager() && Manager)
        {
            GameObject explosion = Manager.GetPooledExplosion();
            explosion.transform.SetPositionAndRotation(transform.position, transform.rotation);
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
