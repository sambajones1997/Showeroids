using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
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
}
