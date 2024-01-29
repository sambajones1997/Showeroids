using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLeaderboardPopUPUI : MonoBehaviour
{
    [SerializeField] GameObject mSettingsMenuCanvas;
    [SerializeField] GameObject mPopUpCanvas;

    public void OnCancelButtonPressed()
    {
        ClosePopUp();
    }

    public void OnConfirmButtonPressed()
    {
        // Reset Leaderboard
        ShoweroidsFileSystem.ResetLeaderBoard();
        ClosePopUp();
    }

    private void ClosePopUp()
    {
        mPopUpCanvas.SetActive(false);
        mSettingsMenuCanvas.SetActive(true);
    }
}
