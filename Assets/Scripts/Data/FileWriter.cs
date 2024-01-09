using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ShoweroidsFileSystem
{
    static string DirectoryPath = "/Data";
    static string ShipDataLocation = "/ShipData.txt";
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
}
