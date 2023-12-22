using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    AsyncOperation LoadSceneOperation;

    public void Start()
    {
        
    }

    public void OnPlayPressed()
    {
        LoadSceneOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        LoadSceneOperation.allowSceneActivation = true;
    }

    public void OnCustomiseShipPressed()
    {
        LoadSceneOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        LoadSceneOperation.allowSceneActivation = true;
    }

    public void OnLeaderboardPressed()
    {

    }

    public void OnHelpPressed()
    {

    }

    public void OnQuitPressed()
    {
        Application.Quit(0);
    }

    public void OnSettingsPressed()
    {

    }
}
