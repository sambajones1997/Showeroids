using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] Sprite mShipLifeSprite;

    [SerializeField] SpriteRenderer mShipBodySprite;
    [SerializeField] SpriteRenderer mShipTrim1Sprite;
    [SerializeField] SpriteRenderer mShipTrim2Sprite;
    [SerializeField] SpriteRenderer mShipDetailsSprite;
    [SerializeField] SpriteRenderer mShipThrustSprite;

    [Header("Extra Sprites")]
    [SerializeField] SpriteRenderer mShipDamagedSprite;

    [SerializeField] GameObject mBodyColourLocation;
    [SerializeField] GameObject mTrim1ColourLocation;
    [SerializeField] GameObject mTrim2ColourLocation;

    [SerializeField] float ThrustMinScale = 0.75f;
    [SerializeField] float ThrustMaxScale = 1.0f;

    public SpriteRenderer GetThrustSprite()
    {
        return mShipThrustSprite;
    }

    public void EnableThrustSprite(bool bEnable)
    {
        GetThrustSprite().enabled = bEnable;

        if (bEnable)
        {
            StartCoroutine("ThrustUpdate");
        }
        else
        {
            StopCoroutine("ThrustUpdate");
        }
    }

    public void EnableAllSprites(bool bEnabled, bool bEnableDamaged = false)
    {
        if (mShipBodySprite)
        {
            mShipBodySprite.enabled = bEnabled;
        }

        if (mShipTrim1Sprite)
        {
            mShipTrim1Sprite.enabled = bEnabled;
        }

        if (mShipTrim2Sprite)
        {
            mShipTrim2Sprite.enabled = bEnabled;
        }

        if (mShipDetailsSprite)
        {
            mShipDetailsSprite.enabled = bEnabled;
        }

        if (mShipThrustSprite)
        {
            mShipThrustSprite.enabled = bEnabled;
        }

        EnableDamageSprite(bEnabled && bEnableDamaged);
    }

    public void EnableDamageSprite(bool bEnabled)
    {
        mShipDamagedSprite.enabled = bEnabled;
    }

    private IEnumerator ThrustUpdate()
    {
        while (mShipThrustSprite && mShipThrustSprite.enabled)
        {
            mShipThrustSprite.transform.localScale = new Vector3(1.0f, Random.Range(ThrustMinScale, ThrustMaxScale), 1.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public SpriteRenderer GetShipBodySprite()
    {
        return mShipBodySprite;
    }

    public SpriteRenderer GetShipTrim1Sprite()
    {
        return mShipTrim1Sprite;
    }

    public SpriteRenderer GetShipTrim2Sprite()
    {
        return mShipTrim2Sprite;
    }

    public SpriteRenderer GetShipDetailsSprite()
    {
        return mShipDetailsSprite;
    }

    public Sprite GetShipLifeSprite()
    {
        return mShipLifeSprite;
    }

    public GameObject GetShipColourAttach()
    {
        if (mBodyColourLocation)
        {
            return mBodyColourLocation;
        }
        return null;
    }

    public GameObject GetTrim1ColourAttach()
    {
        if (mTrim1ColourLocation)
        {
            return mTrim1ColourLocation;
        }
        return null;
    }

    public GameObject GetTrim2ColourAttach()
    {
        if (mTrim2ColourLocation)
        {
            return mTrim2ColourLocation;
        }
        return null;
    }

    public void DisableRendering()
    {
        EnableAllSprites(false);
    }

    public void EnableRendering()
    {
        EnableAllSprites(true);
    }
}
