using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI FinalScoreText;
    [SerializeField] PlayerScore PlayerScoreComp;

    public void Start()
    {
        if (PlayerScoreComp)
        {
            SetFinalScore((int)PlayerScoreComp.GetCurrentScore());
        }
    }

    public void SetFinalScore(int FinalScore)
    {
        FinalScoreText.text = FinalScore.ToString();
    }
}
