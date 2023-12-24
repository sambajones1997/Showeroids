using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject mSettingsCanvas;
    [SerializeField] GameObject mMainMenuCanvas;
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
        mSettingsCanvas.SetActive(true);
        mMainMenuCanvas.SetActive(false);
    }

    public void ShowCanvas(bool showCanvas)
    {
        mMainMenuCanvas.SetActive(true);
    }
}
