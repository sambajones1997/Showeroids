
public static class GameData
{
    public static LevelData GameLevelData { get; set; }
    public static float CurrentScore { get; set; }
    public static int CurrentScoreTrunc { get; private set; }

    public static void SetCurrentScore(float score)
    {
        CurrentScore = score;
        CurrentScoreTrunc = (int)score;
    }

    public static int GetLevelIndex() { return GameLevelData.LevelIndex; }

    public static int GetLevelDifficulty() { return GameLevelData.DifficultyLevel; }

    public static int IncrementLevelDifficulty()
    {
        GameLevelData.DifficultyLevel++;

        if (GameLevelData.DifficultyLevel > 5)
        {
            GameLevelData.DifficultyLevel = 5;
        }

        return GameLevelData.DifficultyLevel;
    }
}
