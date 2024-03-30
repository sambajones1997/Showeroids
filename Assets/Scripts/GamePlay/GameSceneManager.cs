using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSceneManager : MonoBehaviour
{
    public enum EGameState { Intro, Playing, Outro, Ended, Dead }

    EGameState mGameState = EGameState.Intro;
    PlayerShip mPlayerShip;
    PlayerController mPlayerController;
    PlayerScore mPlayerScore;

    float mLevelTimer = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (mGameState == EGameState.Playing)
        {
            mLevelTimer -= Time.deltaTime;

            if (mLevelTimer <= 0.0f)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        mGameState = EGameState.Outro;
        mPlayerController.SetInputEnabled(false);
        mPlayerScore.bUpdateScore = false;
    }

    public void SetActivePlayer(PlayerShip ship, PlayerController controller, PlayerScore score)
    {
        mPlayerShip = ship;
        mPlayerController = controller;
        mPlayerScore = score;
    }

    public void IntroOver()
    {
        mGameState = EGameState.Playing;
    }
}
