public class PlayerData
{
    public int mShipIndex = 3;
    public int mMainBodyColour = 3;
    public int mTrim1Colour = 0;
    public int mTrim2Colour = 7;

    public PlayerData(int shipIndex = 0, int mainBody = 0, int trim1Colour = 0, int trim2Colour = 0)
    {
        mShipIndex = shipIndex;
        mMainBodyColour = mainBody;
        mTrim1Colour = trim1Colour;
        mTrim2Colour = trim2Colour;
    }
}
