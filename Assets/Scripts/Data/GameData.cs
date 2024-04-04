public static class GameData
{
    public static LevelData GameLevelData = new LevelData(0, 0);
    public static float CurrentScore = 0.0f;
    public static int CurrentScoreTrunc = 0;

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
