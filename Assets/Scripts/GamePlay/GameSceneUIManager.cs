using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUIManager : MonoBehaviour
{
    [SerializeField] GameObject GameCanvas;
    [SerializeField] GameObject LeaderBoardCanvas;
    [SerializeField] GameObject PlayerUICanvas;

    [SerializeField] GameObject GameOverObject;
    [SerializeField] GameObject LevelCompletedObject;

    public void OnPlayerScoreLogged()
    {
        LeaderBoardCanvas.SetActive(true);
        PlayerUICanvas.SetActive(false);
    }
}
