using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSceneUI : MonoBehaviour
{
    [SerializeField] GameObject m_ScoreObject;
    [SerializeField] GameObject m_LifeObject;
    [SerializeField] TMPro.TextMeshProUGUI m_ScoreText;
    [SerializeField] SpriteRenderer m_Life0Sprite;
    [SerializeField] SpriteRenderer m_Life1Sprite;
    [SerializeField] SpriteRenderer m_Life2Sprite;

    [SerializeField] PlayerController m_PlayerController;
    [SerializeField] PlayerScore m_PlayerScore;
    [SerializeField] GameObject m_LevelCompletedObject;
    [SerializeField] GameObject m_GameOverObject;
    // Start is called before the first frame update

    private void Start()
    {
        if (m_PlayerController)
        {
            m_PlayerController.PlayerTakenHitEvent.AddListener(PlayerLivesChanged);

            RefreshLives();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerScore)
        {
            m_ScoreText.text = m_PlayerScore.m_CurrentScore.ToString();
        }
    }

    public void OnGameOver()
    {
        DisplayGameOver(true);
    }

    public void OnLevelCompleted()
    {
        DisplayLevelCompleted(true);
    }

    void DisplayLevelCompleted(bool bDisplay = true)
    {
        if (m_LevelCompletedObject)
        {
            m_LevelCompletedObject.SetActive(bDisplay);
        }
    }

    void DisplayGameOver(bool bDisplay = true)
    {
        if (m_GameOverObject)
        {
            m_GameOverObject.SetActive(bDisplay);
        }
    }

    void RefreshLives()
    {
        int lives = 3;
        if (m_PlayerController)
        {
            lives = m_PlayerController.GetCurrentLives();
        }    

        if (m_Life0Sprite.sprite && lives < 1)
        {
            m_Life0Sprite.enabled = false;
        }

        if (m_Life1Sprite.sprite && lives < 2)
        {
            m_Life1Sprite.enabled = false;
        }

        if (m_Life2Sprite.sprite && lives < 3)
        {
            m_Life2Sprite.enabled = false;
        }
    }

    private void PlayerLivesChanged()
    {
        if (m_PlayerController)
        {
            switch (m_PlayerController.GetCurrentLives())
            {
                case 0:
                    m_Life0Sprite.enabled = false;
                    break;
                case 1:
                    m_Life1Sprite.enabled = false;
                    break;
                case 2:
                    m_Life2Sprite.enabled = false;
                    break;
            }
        }
    }

    public void DisplayUI(bool bEnabled = true)
    {
        m_ScoreObject.SetActive(bEnabled);
        m_LifeObject.SetActive(bEnabled);

        if (m_PlayerController && m_Life0Sprite == null)
        {
            m_Life0Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();
            m_Life1Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();
            m_Life2Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();
        }
    }
}
