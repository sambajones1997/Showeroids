using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomiserUI : MonoBehaviour
{
    [Header("Ship Renderer")]
    [SerializeField] List<PlayerShip> mShipList = new List<PlayerShip>();
    SpriteRenderer mShipBodySpriteRenderer;
    SpriteRenderer mShipTrim1SpriteRenderer;
    SpriteRenderer mShipTrim2SpriteRenderer;
    [Header("Colour Choosers")]
    [SerializeField] Image mColourSpriteRenderer;
    [SerializeField] Image mTrim1SpriteRenderer;
    [SerializeField] Image mTrim2SpriteRenderer;
    [SerializeField] GameObject mColourLineObject;
    [SerializeField] GameObject mTrim1ColourLineObject;
    [SerializeField] GameObject mTrim2ColourLineObject;
    [Header("Colours")]
    [SerializeField] List<Color> mColours = new List<Color>();
    [SerializeField] string mTintColourName = "_TintColour";

    AsyncOperation LoadSceneOperation;
    int mColourIndex = 0;
    int mTrim1ColourIndex = 0;
    int mTrim2ColourIndex = 0;
    int mShipIndex = 0;

    private void Start()
    {
        DisplayShip();
        UpdateCustomiserColours();
    }

    private void Update()
    {
        if (mColourLineObject && mTrim1ColourLineObject && mTrim2ColourLineObject)
        {
            if (mShipList.Count > mShipIndex && mShipList[mShipIndex])
            {
                mColourLineObject.transform.position = mShipList[mShipIndex].GetShipColourAttach().transform.position;
                mTrim1ColourLineObject.transform.position = mShipList[mShipIndex].GetTrim1ColourAttach().transform.position;
                mTrim2ColourLineObject.transform.position = mShipList[mShipIndex].GetTrim2ColourAttach().transform.position;
            }
        }

    }

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
        mShipList[mShipIndex].gameObject.SetActive(false);
        if (increment)
        {
            mShipIndex++;
        }
        else
        {
            mShipIndex--;
        }

        if (mShipIndex >= mShipList.Count)
        {
            mShipIndex = 0;
        }
        else if (mShipIndex < 0)
        {
            mShipIndex = mShipList.Count - 1;
        }

        mColours.RemoveAt(0);

        DisplayShip();
        UpdateShipSpriteAndColours();
    }

    private void UpdateShipSpriteAndColours()
    {
        if (mShipBodySpriteRenderer && mShipList.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipList[mShipIndex].gameObject.SetActive(true);
            mShipBodySpriteRenderer.material.SetColor(mTintColourName, mColours[mColourIndex]);
        }

        if (mShipTrim1SpriteRenderer && mShipList.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipTrim1SpriteRenderer.material.SetColor(mTintColourName, mColours[mTrim1ColourIndex]);
        }

        if (mShipTrim2SpriteRenderer && mShipList.Count > mShipIndex && mColours.Count > mColourIndex)
        {
            mShipTrim2SpriteRenderer.material.SetColor(mTintColourName, mColours[mTrim2ColourIndex]);
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

    private void DisplayShip()
    {
        if (mShipList.Count > mShipIndex && mShipIndex >= 0)
        {
            mShipList[mShipIndex].gameObject.SetActive(true);
            mShipBodySpriteRenderer = mShipList[mShipIndex].GetShipBodySprite();
            mShipTrim1SpriteRenderer = mShipList[mShipIndex].GetShipTrim1Sprite();
            mShipTrim2SpriteRenderer = mShipList[mShipIndex].GetShipTrim2Sprite();

            if (mShipBodySpriteRenderer)
            {
                Color startingColour = mShipBodySpriteRenderer.material.GetColor(mTintColourName);
                mColours.Insert(0, startingColour);
            }
        }
    }

    public void OnHomeButtonPressed()
    {
        ReturnToMainMenu();
    }

    public void OnAcceptButtonPressed()
    {
        // Save Ship Out
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        LoadSceneOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        LoadSceneOperation.allowSceneActivation = true;
    }
}
