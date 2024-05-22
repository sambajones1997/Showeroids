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

    [SerializeField] GameObject mBodyColourLocation;
    [SerializeField] GameObject mTrim1ColourLocation;
    [SerializeField] GameObject mTrim2ColourLocation;

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
        if (mShipBodySprite)
        {
            mShipBodySprite.enabled = false;
        }

        if (mShipTrim1Sprite)
        {
            mShipTrim1Sprite.enabled = false;
        }

        if (mShipTrim2Sprite)
        {
            mShipTrim2Sprite.enabled = false;
        }

        if (mShipDetailsSprite)
        {
            mShipDetailsSprite.enabled = false;
        }
    }

    public void EnableRendering()
    {
        if (mShipBodySprite)
        {
            mShipBodySprite.enabled = true;
        }

        if (mShipTrim1Sprite)
        {
            mShipTrim1Sprite.enabled = true;
        }

        if (mShipTrim2Sprite)
        {
            mShipTrim2Sprite.enabled = true;
        }

        if (mShipDetailsSprite)
        {
            mShipDetailsSprite.enabled = true;
        }
    }
}
