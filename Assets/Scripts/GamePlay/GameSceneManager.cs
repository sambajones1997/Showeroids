using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameSceneManager : MonoBehaviour
{
    public enum EGameState { Intro, Playing, Outro, Ended, Dead }
    [Header("Component References")]
    [SerializeField] GameObject Destination;
    [SerializeField] GameSceneUI mGameSceneUI;
    [SerializeField] AsteroidPoolingManager mAsteroidPoolingManager;
    [SerializeField] PlayerController mPlayerController;
    [SerializeField] PlayerScore mPlayerScore;

    [Header("Game Scene Manager")]

    public float mLevelTimer = 30.0f;
    public float[] mLevelTimes = { 30.0f, 45.0f, 60.0f };
    EGameState mGameState = EGameState.Intro;

    [Header("Game Manager Events")]
    [SerializeField] UnityEvent LevelOutroCompletedEvent;
    [SerializeField] UnityEvent LevelIntroCompletedEvent;
    [SerializeField] UnityEvent LevelStartedPlayingEvent;
    [SerializeField] UnityEvent LevelOutroStartedEvent;
    [SerializeField] UnityEvent LevelEndedEvent;

    private void Start()
    {
        mPlayerController.PlayerDiedEvent.AddListener(OnPlayerDied);
    }

    private void FixedUpdate()
    {
        if (mGameState == EGameState.Playing)
        {
            mLevelTimer -= Time.fixedDeltaTime;

            if (mLevelTimer <= 0.0f)
            {
                mGameState = EGameState.Outro;

                Destination.GetComponent<SimpleSpriteMover>().enabled = true;
            }
        }
        else if (mGameState == EGameState.Outro)
        {
            if (!mAsteroidPoolingManager || (mAsteroidPoolingManager && mAsteroidPoolingManager.GetNumActiveAsteroids() <= 0))
            {
                mPlayerController.LevelOver();
                SimpleSpriteMover PlayerEndMover = mPlayerController.gameObject.GetComponentInChildren<SimpleSpriteMover>();

                if (PlayerEndMover)
                {
                    PlayerEndMover.SpriteMovementCompleted.AddListener(OutroOver);
                }
            }
        }
    }

    void EndGame()
    {
        LevelOutroCompletedEvent.Invoke();
        mGameState = EGameState.Ended;
    }

    void OutroOver()
    {
        EndGame();
    }

    public void IntroOver()
    {
        mGameState = EGameState.Playing;
        mLevelTimer = mLevelTimes[GameData.GetLevelDifficulty()];

        LevelIntroCompletedEvent.Invoke();
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
                nextLevelIndex = (GameData.GetLevelIndex() + 1) > numDestinations - 1 ? 0 : GameData.GetLevelIndex() + 1;

                if (nextLevelIndex == 0)
                {
                    GameData.IncrementLevelDifficulty();
                }
            }
        }

        GameData.SetCurrentScore(mPlayerScore.GetCurrentScore());
        GameData.GameLevelData.LevelIndex = nextLevelIndex;

        PlayerPrefs.SetInt(PlayerScoreStatics.CurrentSessionScore, (int)mPlayerScore.GetCurrentScore());
        PlayerPrefs.Save();

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

    public void Retry()
    {
        GameData.SetCurrentScore(0);
        GameData.GameLevelData.LevelIndex = 0;
        GameData.GameLevelData.DifficultyLevel = 0;

        PlayerPrefs.SetInt(PlayerScoreStatics.CurrentSessionScore, 0);
        PlayerPrefs.Save();

        AdvanceToNextLevel();
    }

    public void GameOver()
    {
        if (mPlayerController)
        {
            mPlayerController.LevelOver();
        }

        if (mGameSceneUI)
        {
            mGameSceneUI.OnGameOver();
        }

        if (mPlayerScore)
        {
            mPlayerScore.SetScoreEnabled(false);
        }

        mGameState = EGameState.Dead; 
    }

    public EGameState GetLevelGameState()
    {
        return mGameState;
    }

    private void OnPlayerDied()
    {
        GameOver();
    }
}
