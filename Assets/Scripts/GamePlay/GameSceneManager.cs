using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameSceneManager : MonoBehaviour
{
    public enum EGameState { Intro, Playing, Outro, Ended, Dead }
    [SerializeField] GameObject Destination;
    EGameState mGameState = EGameState.Intro;
    PlayerShip mPlayerShip;
    PlayerController mPlayerController;
    PlayerScore mPlayerScore;

    public float mLevelTimer = 30.0f;


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
        mPlayerController.LevelOver();
        mPlayerScore.bUpdateScore = false;

        Destination.GetComponent<SimpleSpriteMover>().enabled = true;
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

    public void EndLevel()
    {
        int nextLevelIndex = 0;
        if (Destination != null)
        {
            DestinationSelector destinationSelector = Destination.GetComponentInChildren<DestinationSelector>();
            if (destinationSelector)
            {
                int numDestinations = destinationSelector.GetNumDestinations();
                nextLevelIndex = (GameData.GetLevelIndex() + 1) > numDestinations - 1 ? GameData.GetLevelIndex() + 1 : 0;

                if (nextLevelIndex == 0)
                {
                    GameData.IncrementLevelDifficulty();
                }
            }
        }

        GameData.SetCurrentScore(mPlayerScore.GetCurrentScore());
        GameData.GameLevelData.LevelIndex = nextLevelIndex;

        AdvanceToNextLevel();
    }

    public void AdvanceToNextLevel()
    {
        AsyncOperation SceneLoader = SceneManager.LoadSceneAsync(1);
        SceneLoader.allowSceneActivation = true;
    }

    public void ReturnToMainMenu()
    {
        AsyncOperation SceneLoader = SceneManager.LoadSceneAsync(0);
        SceneLoader.allowSceneActivation = true;
    }
}
