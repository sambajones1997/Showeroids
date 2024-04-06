using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Rigidbody RigidBody;
    [SerializeField] GameObject ImpactVFX;
    [SerializeField] Vector3 MovementVector = Vector3.zero;
    [SerializeField] float MinSpeed = 1.0f;
    [SerializeField] float MaxSpeed = 1.0f;

    float CurrentSpeed = 0.0f;

    private void Start()
    {
        MovementVector = new Vector3(Random.Range(-0.5f, 0.5f), -1.0f, 0.0f).normalized;
    }

    private void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + (MovementVector * CurrentSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ImpactVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
