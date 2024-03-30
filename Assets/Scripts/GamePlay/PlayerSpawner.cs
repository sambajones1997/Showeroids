using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject mPlayerRoot;
    [SerializeField] List<GameObject> Ships = new List<GameObject>();
    [SerializeField] List<Color> mColours = new List<Color>();
    [SerializeField] string mTintColourName = "_TintColour";
    PlayerShip mShip;

    private void Start()
    {
        PlayerData playerData = ShoweroidsFileSystem.ReadShipDataFile();

        GameObject ship = Instantiate(Ships[playerData.mShipIndex], mPlayerRoot.transform);
        mShip = ship.GetComponent<PlayerShip>();

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
