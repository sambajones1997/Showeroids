using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public int mMaxLives = 3;
    public int mLives = 3;

    [Header("Player Controls")]
    [SerializeField] const float maxSpeed = 5.0f;
    [SerializeField] float speed = 0.0f;
    [SerializeField] float DamageInvincibility = 0.5f;

    float horizontalInput = 0.0f;

    float horizontalBounds = 8.5f;

    float DamageInvincibilityTimer = 0.0f;

    [SerializeField] float deadzone = 0.1f;
    [SerializeField] float deccelerationSpeed = 2.0f;
    [SerializeField] float deccelerationMultiplier = 2.0f;
    [SerializeField] float speedSnap = 0.5f;

    [SerializeField] protected bool bIsInputEnabled = true;
    protected bool bCanTakeDamage = true;

    [Header("Player Prefabs")]
    [SerializeField] GameObject mExplosionVFX;

    [Header("Component References")]
    [SerializeField] PlayerScore mPlayerScore;
    [SerializeField] PlayerShip mPlayerShip;
    [SerializeField] AudioSource mAudioSource;

    [Header("Unity Events")]
    [SerializeField] public UnityEvent PlayerDiedEvent;
    [SerializeField] public UnityEvent PlayerTakenHitEvent;

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

    private void FixedUpdate()
    {
        if (!bIsInputEnabled)
        {
            return;
        }

        if (!bCanTakeDamage && mLives > 0 && DamageInvincibilityTimer > 0.0f)
        {
            DamageInvincibilityTimer -= Time.fixedDeltaTime;

            if (DamageInvincibilityTimer < 0.0f)
            {
                bCanTakeDamage = true;

                if (mPlayerShip)
                {
                    mPlayerShip.EnableAllSprites(!mPlayerShip.GetShipBodySprite().enabled, false);
                    mPlayerShip.EnableDamageSprite(false);
                }
                StopCoroutine("ShipHitEffects");
            }
        }
    }

    public void EnableThrusterSprite()
    {
        if (mPlayerShip)
        {
            mPlayerShip.EnableThrustSprite(true);
        }
    }

    void CacheInput()
    {
        if (!bIsInputEnabled)
        {
            horizontalInput = 0.0f;
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

        if (InputEnabled)
        {
            mPlayerScore.enabled = true;
            mPlayerScore.SetScoreEnabled(true);
        }
        else
        {
            mPlayerScore.SetScoreEnabled(false);
        }
    }

    public void LevelOver()
    {
        if (mLives > 0)
        {
            SimpleSpriteMover LevelOverSpriteMover = GetComponent<SimpleSpriteMover>();
            if (LevelOverSpriteMover)
            {
                LevelOverSpriteMover.enabled = true;
            }
        }

        SetInputEnabled(false);

        bCanTakeDamage = false;

        if (mPlayerScore)
        {
            mPlayerScore.SetScoreEnabled(false);
            mPlayerScore.SaveOutCurrentScore();
        }

        Collider SpriteCollider = GetComponentInChildren<Collider>();
        if (SpriteCollider)
        {
            SpriteCollider.enabled = false;
        }
    }

    public void TakeHit()
    {
        if (!bCanTakeDamage)
        {
            return;
        }

        mLives--;
        DamageInvincibilityTimer = DamageInvincibility;
        bCanTakeDamage = false;

        if (mAudioSource)
        {
            mAudioSource.Play();
        }

        if (mLives <= 0)
        {
            PlayerDiedEvent.Invoke();
            DamageInvincibility = float.MaxValue;
            mPlayerShip.DisableRendering(); // Set invisible
            if (mExplosionVFX)
            {
                Instantiate(mExplosionVFX, transform);
            }
        }
        else
        {
            StartCoroutine("ShipHitEffects");
        }

        PlayerTakenHitEvent.Invoke();
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

    IEnumerator ShipHitEffects()
    {
        while (DamageInvincibilityTimer > 0.0f)
        {
            if (mPlayerShip)
            {
                mPlayerShip.EnableDamageSprite(true);
                mPlayerShip.EnableAllSprites(!mPlayerShip.GetShipBodySprite().enabled, true);
            }
            yield return new WaitForSeconds(0.1f);
        }      
    }
}
