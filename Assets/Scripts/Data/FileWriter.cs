using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ShoweroidsFileSystem
{
    static string DirectoryPath = "/Data";
    static string ShipDataLocation = "/ShipData.txt";
    static string LeaderBoardLocation = "/Leaderboard.txt";
    public static void WriteShipDataFile(PlayerData shipPlayerData)
    {
        string path = Application.persistentDataPath + DirectoryPath;

        //Write some text to the test.txt file
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);       
        }

        StreamWriter writer = File.CreateText(path + ShipDataLocation);
        writer.WriteLine(shipPlayerData.mShipIndex + ",");
        writer.WriteLine(shipPlayerData.mMainBodyColour + ",");
        writer.WriteLine(shipPlayerData.mTrim1Colour + ",");
        writer.WriteLine(shipPlayerData.mTrim2Colour);

        writer.Flush();
        writer.Close();
    }

    public static PlayerData ReadShipDataFile()
    {
        string path = Application.persistentDataPath + DirectoryPath + ShipDataLocation;
        PlayerData playerData = new PlayerData();
        //Write some text to the test.txt file

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path, true);
            string dataRead = reader.ReadToEnd();
            string[] playerDataString = dataRead.Split(new char[] { ',' });

            if (playerDataString.Length >= 4)
            {
                int.TryParse(playerDataString[0], out playerData.mShipIndex);
                int.TryParse(playerDataString[1], out playerData.mMainBodyColour);
                int.TryParse(playerDataString[2], out playerData.mTrim1Colour);
                int.TryParse(playerDataString[3], out playerData.mTrim2Colour);
            }
            reader.Close();
        }

        return playerData;
    }

    public static void ResetLeaderBoard()
    {
        List<SLeaderBoardUserData> leaderBoardUserData = new List<SLeaderBoardUserData>();
        WriteLeaderBoard(ref leaderBoardUserData);
    }

    public static void WriteLeaderBoard(ref List<SLeaderBoardUserData> leaderBoardUserData)
    {
        string path = Application.persistentDataPath + DirectoryPath;

        //Write some text to the test.txt file
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        int maxLeaderboardEntries = 10;

        StreamWriter writer = File.CreateText(path + ShipDataLocation);
        foreach (SLeaderBoardUserData userData in leaderBoardUserData)
        {
            writer.WriteLine(userData.mName + "," + userData.mScore);
            maxLeaderboardEntries--;

            // Lets limit our leaderbaord entries to just what will be displayed
            if (maxLeaderboardEntries <= 0)
            {
                break;
            }
        }

        writer.Flush();
        writer.Close();
    }

    public static void ReadLeaderBoard(out List<SLeaderBoardUserData> leaderBoardUserData)
    {
        List<SLeaderBoardUserData> dataList = new List<SLeaderBoardUserData>();

        string path = Application.persistentDataPath + DirectoryPath + LeaderBoardLocation;

        //Write some text to the test.txt file
        if (File.Exists(path))
        {
            FileStream fileStream = File.OpenRead(path);
            string[] leaderBoardEntries = File.ReadAllLines(path);
            fileStream.Close();

            foreach(string entry in leaderBoardEntries)
            {
                string[] leaderBoardDataString = entry.Split(new char[] { ',' });

                if (leaderBoardDataString.Length == 2)
                {
                    int leaderBoardScore = 0;
                    int.TryParse(leaderBoardDataString[0], out leaderBoardScore);
                    string leaderBoardName = leaderBoardDataString[1];

                    dataList.Add(new SLeaderBoardUserData(leaderBoardName, leaderBoardScore));
                }
            }
        }
        else
        {
            File.CreateText(path);
        }

        leaderBoardUserData = dataList;
    }
}
