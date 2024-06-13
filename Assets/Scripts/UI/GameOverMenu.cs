using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Button AcceptButton;
    [SerializeField] TMPro.TextMeshProUGUI FinalScoreText;
    [SerializeField] TMPro.TMP_InputField NameInputField;
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

    public void SaveOutFinalScore()
    {
        if (NameInputField && PlayerScoreComp)
        {
            ShoweroidsFileSystem.AddLeaderBoardEntry(NameInputField.text, (int)PlayerScoreComp.GetCurrentScore());
        }
    }

    public void EnableDisableAcceptButton()
    {
        if (AcceptButton)
        {
            AcceptButton.interactable = NameInputField.text.Length > 0;
        }
    }
}
