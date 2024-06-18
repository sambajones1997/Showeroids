using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerScoreStatics
{
    public static string CurrentSessionScore = "CurrentSessionScore";
}

public class PlayerScore : MonoBehaviour
{
    public int m_CurrentScore;
    public float m_Score;
    public float m_ScoreMultiplier = 1.0f;
    public float m_DifficultyMultiplier = 1.0f;

    public int m_impactPenalty = -100;
    public int m_scorePerSecond = 20;
    public int m_newLifeScore = 10000;

    public float m_timeSinceLastHit = 0.0f;

    int LastNewLifeScore = 0;

    public bool bUpdateScore = false;

    public List<KeyValuePair<float, float>> m_MultiplierTimes = new List<KeyValuePair<float, float>>();

    public UnityEvent NewLifeScore;

    private void Start()
    {
        StartCoroutine("UpdateTimeSinceHit");

        m_DifficultyMultiplier = GameData.GetLevelDifficulty() + 1.0f;

        if (GameData.GetLevelDifficulty() <= 0 && GameData.GetLevelIndex() <= 0)
        {
            PlayerPrefs.SetInt(PlayerScoreStatics.CurrentSessionScore, 0);
            m_Score = 0;
            PlayerPrefs.Save();
        }
        else
        {
            m_Score = PlayerPrefs.GetInt(PlayerScoreStatics.CurrentSessionScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bUpdateScore)
        {
            return;
        }

        AddScore(m_scorePerSecond * Time.deltaTime);

        if (m_CurrentScore - LastNewLifeScore >= m_newLifeScore)
        {
            NewLifeScore.Invoke();
            // Round to nearest 1000
            LastNewLifeScore = (m_CurrentScore / 1000) * 1000;
        }
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
        PlayerPrefs.SetInt(PlayerScoreStatics.CurrentSessionScore, m_CurrentScore);
        PlayerPrefs.Save();
    }

    public void RevertCurrentSessionScore()
    {
        PlayerPrefs.SetInt(PlayerScoreStatics.CurrentSessionScore, 0);
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

    public float MultiplyScore(float score)
    {
        return score * (m_ScoreMultiplier + m_DifficultyMultiplier);
    }

    public void AddScore(float score)
    {
        m_Score += MultiplyScore(score);
        m_CurrentScore = (int)(m_Score);
    }
}
