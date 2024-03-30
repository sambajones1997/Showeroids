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
    // Start is called before the first frame update

    private void OnEnable()
    {
        m_Life0Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();
        m_Life1Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();
        m_Life2Sprite.sprite = m_PlayerController.GetPlayerShip().GetShipLifeSprite();

        m_ScoreObject.SetActive(true);
        m_LifeObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.text = m_PlayerScore.m_CurrentScore.ToString();
    }

    private void FixedUpdate()
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
}
