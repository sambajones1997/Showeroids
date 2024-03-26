using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] List<PlayerShip> Ships = new List<PlayerShip>();
    [SerializeField] List<Color> mColours = new List<Color>();
    [SerializeField] string mTintColourName = "_TintColour";
    PlayerShip mShip;

    private void Start()
    {
        PlayerData playerData = ShoweroidsFileSystem.ReadShipDataFile();

        mShip = Ships[playerData.mShipIndex];

        mShip.gameObject.SetActive(true);

        if (mShip.GetShipBodySprite())
        {
            Color startingColour = mShip.GetShipBodySprite().material.GetColor(mTintColourName);
            mColours.Insert(0, startingColour);
        }

        mShip.GetShipBodySprite().material.SetColor(mTintColourName, mColours[playerData.mMainBodyColour]);
        mShip.GetShipTrim1Sprite().material.SetColor(mTintColourName, mColours[playerData.mTrim1Colour]);
        mShip.GetShipTrim2Sprite().material.SetColor(mTintColourName, mColours[playerData.mTrim2Colour]);
    }

    public void EnablePlayerControl()
    {
       // mShip.GetComponent<SimpleSpriteMover>().enabled = true;
    }
}
