using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public int mMaxLives = 3;
    public int mLives = 3;

    [Header("Player Controls")]
    [SerializeField] const float maxSpeed = 5.0f;
    [SerializeField] float speed = 0.0f;

    float horizontalInput = 0.0f;

    float horizontalBounds = 8.5f;

    [SerializeField] float deadzone = 0.1f;
    [SerializeField] float deccelerationSpeed = 2.0f;
    [SerializeField] float deccelerationMultiplier = 2.0f;
    [SerializeField] float speedSnap = 0.5f;

    protected bool bIsInputEnabled = true;

    [Header("Component References")]
    [SerializeField] PlayerScore mPlayerScore;
    [SerializeField] PlayerShip mPlayerShip;
    [SerializeField] GameSceneManager mGameSceneManager;

    private void OnEnable()
    {
        mPlayerScore.enabled = true;
        mPlayerScore.bUpdateScore = true;
        mGameSceneManager.IntroOver();
        mGameSceneManager.SetActivePlayer(mPlayerShip, this, mPlayerScore);
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
            speed = 0.0f;
        }
        else if (horizontal.x >= horizontalBounds)
        {
            horizontal.x = horizontalBounds;
            speed = 0.0f;
        }

        transform.position = horizontal;
    }

    void CacheInput()
    {
        if (!bIsInputEnabled)
        {
            return;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        // Cache input from Gyro or UI
        horizontalInput = Mathf.Clamp(Input.gyro.attitude.eulerAngles.x, -1.0f, 1.0f);
#else
        horizontalInput = Input.GetAxisRaw("Horizontal");
#endif //UNITY_ANDROID
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

    public void SetInputEnabled(bool InputEnabled = true)
    {
        bIsInputEnabled = InputEnabled;
    }

    public void LevelOver()
    {
        GetComponent<SimpleSpriteMover>().enabled = true;
        SetInputEnabled(false);
    }

    public void TakeHit()
    {
        mLives--;
    }

    public int GetCurrentLives()
    {
        return mLives;
    }

    public PlayerShip GetPlayerShip()
    {
        return mPlayerShip;
    }

    public void SetPlayerShip(PlayerShip ship)
    {
        mPlayerShip = ship;
    }
}
