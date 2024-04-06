using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] GameObject ImpactVFX;
    [SerializeField] Vector3 MovementVector = Vector3.zero;
    [SerializeField] float MinSpeed = 1.0f;
    [SerializeField] float MaxSpeed = 1.0f;

    float CurrentSpeed = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ImpactVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
