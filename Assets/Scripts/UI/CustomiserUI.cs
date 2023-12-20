using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomiserUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer mShipSpriteRenderer;
    [SerializeField] Image mColourSpriteRenderer;
    [SerializeField] List<Sprite> mShips = new List<Sprite>();
    [SerializeField] List<Color> mColors = new List<Color>();

    int mColorIndex = 0;
    int mShipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNextColourPressed(bool increment = true)
    {
        if (increment)
        {
            mColorIndex++;
        }
        else
        {
            mColorIndex--;
        }

        if (mColorIndex >= mColors.Count)
        {
            mColorIndex = 0;
        }
        else if (mColorIndex < 0)
        {
            mColorIndex = mColors.Count - 1;
        }

        if (mColors.Count > mColorIndex)
        {
            if (mShipSpriteRenderer)
            {
                mShipSpriteRenderer.color = mColors[mColorIndex];
            }
  
            if (mColourSpriteRenderer)
            {
                mColourSpriteRenderer.color = mColors[mColorIndex];
            }
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

        if (mShipSpriteRenderer && mShips.Count > mShipIndex && mColors.Count > mColorIndex)
        {
            mShipSpriteRenderer.sprite = mShips[mShipIndex];
            mShipSpriteRenderer.color = mColors[mColorIndex];
        }
    }
}
