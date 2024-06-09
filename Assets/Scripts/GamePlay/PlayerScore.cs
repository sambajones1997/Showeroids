using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int m_CurrentScore;
    public float m_Score;
    public float m_ScoreMultiplier = 1.0f;
    public float m_DifficultyMultiplier = 1.0f;

    public int m_impactPenalty = -100;
    public int m_scorePerSecond = 20;

    public float m_timeSinceLastHit = 0.0f;

    public bool bUpdateScore = false;

    public List<KeyValuePair<float, float>> m_MultiplierTimes = new List<KeyValuePair<float, float>>();

    private string CurrentSessionScore = "CurrentSessionScore";

    private void Start()
    {
        StartCoroutine("UpdateTimeSinceHit");

        m_DifficultyMultiplier = GameData.GetLevelDifficulty() + 1.0f;

        if (GameData.GetLevelDifficulty() <= 0 && GameData.GetLevelIndex() <= 0)
        {
            PlayerPrefs.SetInt(CurrentSessionScore, 0);
            m_Score = 0;
        }
        else
        {
            m_Score = PlayerPrefs.GetInt(CurrentSessionScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bUpdateScore)
        {
            return;
        }

        m_Score += (m_scorePerSecond * (m_ScoreMultiplier + m_DifficultyMultiplier)) * Time.deltaTime;
        m_CurrentScore = (int)(m_Score);
    }

    private void FixedUpdate()
    {
        m_timeSinceLastHit += Time.fixedDeltaTime;
    }

    public void ShipHit()
    {
        m_timeSinceLastHit = 0.0f;
        m_ScoreMultiplier = 1.0f;
        m_Score -= m_impactPenalty;
    }

    public void LevelEnded()
    {
        bUpdateScore = false;
        StopAllCoroutines();
    }

    public float GetCurrentScore()
    {
        return m_CurrentScore;
    }

    public void SetScoreEnabled(bool bEnabled)
    {
        bUpdateScore = bEnabled;
    }

    public void SaveOutCurrentScore()
    {
        PlayerPrefs.SetInt(CurrentSessionScore, m_CurrentScore);
        PlayerPrefs.Save();
    }

    public void RevertCurrentSessionScore()
    {
        PlayerPrefs.SetInt(CurrentSessionScore, 0);
        PlayerPrefs.Save();
    }

    IEnumerator UpdateTimeSinceHit()
    {
        if (m_timeSinceLastHit > 1.0f)
        {
            m_ScoreMultiplier += 0.1f;
            m_timeSinceLastHit = 0.0f;
        }
        yield return new WaitForSeconds(1.0f);
    }
}
