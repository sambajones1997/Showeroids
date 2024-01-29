using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardEntry : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI mLeaderBoardScore;
    [SerializeField] TMPro.TextMeshProUGUI mLeaderBoardName;

    public TMPro.TextMeshProUGUI GetLeaderBoardEntryNameText()
    {
        return mLeaderBoardName;
    }

    public TMPro.TextMeshProUGUI GetLeaderBoardEntryScoreText()
    {
        return mLeaderBoardScore;
    }

    public int GetLeaderBoardEntryScore()
    {
        int score = 0;
        int.TryParse(mLeaderBoardScore.text, out score);
        return score;
    }

    public string GetLeaderBoardEntryName()
    {
        return mLeaderBoardName.text;
    }
}
