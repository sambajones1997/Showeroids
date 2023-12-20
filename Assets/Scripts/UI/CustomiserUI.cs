using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomiserUI : MonoBehaviour
{
    [Header("Ship Renderer")]
    [SerializeField] SpriteRenderer mShipBodySpriteRenderer;
    [SerializeField] SpriteRenderer mShipTrim1SpriteRenderer;
    [SerializeField] SpriteRenderer mShipTrim2SpriteRenderer;
    [Header("Colour Choosers")]
    [SerializeField] Image mColourSpriteRenderer;
    [SerializeField] Image mTrim1SpriteRenderer;
    [SerializeField] Image mTrim2SpriteRenderer;
    [Header("Sprites")]
    [SerializeField] List<Sprite> mShips = new List<Sprite>();
    [SerializeField] List<Sprite> mShipTrims1 = new List<Sprite>();
    [SerializeField] List<Sprite> mShipTrims2 = new List<Sprite>();
    [Header("Colours")]
    [SerializeField] List<Color> mColours = new List<Color>();

    int mColourIndex = 0;
    int mTrim1ColourIndex = 0;
    int mTrim2ColourIndex = 0;
    int mShipIndex = 0;

    public void OnNextColourPressed(bool increment = true)
    {
        if (increment)
        {
            mColourIndex++;
        }
        else
        {
            mColourIndex--;
        }

        if (mColourIndex >= mColours.Count)
        {
            mColourIndex = 0;
        }
        else if (mColourIndex < 0)
        {
            mColourIndex = mColours.Count - 1;
        }

        UpdateShipSpriteAndColours();
        UpdateCustomiserColours();
    }

    public void OnNextTrim1ColourPressed(bool increment = true)
    {
        ChangeTrim(ref mTrim1ColourIndex, increment);

        UpdateShipSpriteAndColours();
        UpdateCustomiserColours();
    }

    public void OnNextTrim2ColourPressed(bool increment = true)
    {
        ChangeTrim(ref mTrim2ColourIndex, increment);

        UpdateShipSpriteAndColours();
        UpdateCustomiserColours();
    }

    private void ChangeTrim(ref  int trimIndex, bool increment = true)
    {
        if (increment)
        {
            trimIndex++;
        }
        else
        {
            trimIndex--;
        }

        if (trimIndex >= mColours.Count)
        {
            trimIndex = 0;
        }
        else if (trimIndex < 0)
        {
            trimIndex = mColours.Count - 1;
        }
    }

    public void OnNextShipPressed(bool increment = true)
    {
        if (increment)
        {
            mShipIndex++;
        }
        else
        {
            mShipIndex--;
        }

        if (mShipIndex >= mShips.Count)
        {
            mShipIndex = 0;
        }
        else if (mShipIndex < 0)
        {
            mShipIndex = mShips.Count - 1;
        }

        UpdateShipSpriteAndColours();
    }

    private void UpdateShipSpriteAndColours()
    {
        if (mShipBodySpriteRenderer && mShips.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipBodySpriteRenderer.sprite = mShips[mShipIndex];
            mShipBodySpriteRenderer.color = mColours[mColourIndex];
        }

        if (mShipTrim1SpriteRenderer && mShipTrims1.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipTrim1SpriteRenderer.sprite = mShipTrims1[mShipIndex];
            mShipTrim1SpriteRenderer.color = mColours[mShipIndex];
        }

        if (mShipTrim2SpriteRenderer && mShipTrims2.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipTrim2SpriteRenderer.sprite = mShipTrims2[mShipIndex];
            mShipTrim2SpriteRenderer.color = mColours[mShipIndex];
        }
    }

    private void UpdateCustomiserColours()
    {
        if (mColours.Count > mColourIndex)
        {
            if (mColourSpriteRenderer)
            {
                mColourSpriteRenderer.color = mColours[mColourIndex];
            }
        }

        if (mColours.Count > mTrim1ColourIndex)
        {
            if (mTrim1SpriteRenderer)
            {
                mTrim1SpriteRenderer.color = mColours[mTrim1ColourIndex];
            }
        }

        if (mColours.Count > mTrim2ColourIndex)
        {
            if (mTrim2SpriteRenderer)
            {
                mTrim2SpriteRenderer.color = mColours[mTrim2ColourIndex];
            }
        }
    }
}
