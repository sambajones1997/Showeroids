using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SLeaderBoardUserData
{
    public SLeaderBoardUserData(string name, int score)
    {
        mName = name;
        mScore = score;
    }
    public string mName;
    public int mScore;
}

public class LeaderBoard : MonoBehaviour
{
    List<SLeaderBoardUserData> mUsers = new List<SLeaderBoardUserData>();
    [SerializeField] List<LeaderBoardEntry> mLeaderBoardEntries = new List<LeaderBoardEntry>();


    // Start is called before the first frame update
    void Start()
    {
        ReadLeaderBoard();
    }

    public void SaveLeaverBoard()
    {
        ShoweroidsFileSystem.WriteLeaderBoard(ref mUsers);
    }

    public void DisplayLeaderBoard()
    {
        foreach (LeaderBoardEntry entry in mLeaderBoardEntries)
        {
            entry.gameObject.SetActive(false);
        }

        ReadLeaderBoard();
    }

    public void ReadLeaderBoard()
    {
        ShoweroidsFileSystem.ReadLeaderBoard(out mUsers);

        int leaderBoardIndex = 0;

        foreach (SLeaderBoardUserData user in mUsers)
        {
            if (mLeaderBoardEntries.Count >= leaderBoardIndex)
            {
                mLeaderBoardEntries[leaderBoardIndex].GetLeaderBoardEntryNameText().text = user.mName;
                mLeaderBoardEntries[leaderBoardIndex].GetLeaderBoardEntryScoreText().text = user.mScore.ToString();
                mLeaderBoardEntries[leaderBoardIndex].gameObject.SetActive(true);
            }
        }
    }
}
