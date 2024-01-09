using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] const float maxSpeed = 5.0f;
    [SerializeField] float speed = 0.0f;

    float horizontalInput = 0.0f;

    [SerializeField] float horizontalBounds = 10.0f;

    [SerializeField] float deadzone = 0.1f;
    [SerializeField] float deccelerationSpeed = 2.0f;
    [SerializeField] float deccelerationMultiplier = 2.0f;
    [SerializeField] float speedSnap = 0.5f;

    private void OnEnable()
    {
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        CacheInput();
        ApplyAcceleration(horizontalInput);

        Vector3 horizontal = transform.position + (Vector3.right * speed * Time.deltaTime);

        if (horizontal.x <= -horizontalBounds)
        {
            horizontal.x = -horizontalBounds;
        }
        else if (horizontal.x >= horizontalBounds)
        {
            horizontal.x = horizontalBounds;
        }

        transform.position = horizontal;
    }

    void CacheInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void ApplyAcceleration(float axis)
    {
        float acceleration = 0.0f;
        if (Mathf.Abs(axis) > deadzone)
        {
            acceleration = axis * maxSpeed * Time.deltaTime;
        }
        else if (Mathf.Abs(speed) > 0.0f)
        {
            if (Mathf.Abs(speed) <= speedSnap)
            {
                acceleration = -speed;
            }
            else
            {
                float axisMultiplier = speed > 0.0f ? -1.0f : 1.0f;
                acceleration = deccelerationSpeed * axisMultiplier * deccelerationMultiplier * Time.deltaTime;
            }
        }

        speed = Mathf.Clamp(speed + acceleration, -maxSpeed, maxSpeed);
    }
}
