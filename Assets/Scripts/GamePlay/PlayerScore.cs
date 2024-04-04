using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int m_CurrentScore;
    public float m_Score;
    public float m_ScoreMultiplier = 1.0f;

    public int m_impactPenalty = -100;
    public int m_scorePerSecond = 20;

    public float m_timeSinceLastHit = 0.0f;

    public bool bUpdateScore = true;

    public List<KeyValuePair<float, float>> m_MultiplierTimes = new List<KeyValuePair<float, float>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bUpdateScore)
        {
            return;
        }

        m_Score += (m_scorePerSecond * Time.deltaTime * m_ScoreMultiplier);
        m_CurrentScore = (int)(m_Score);
    }

    public void ShipHit()
    {
        m_ScoreMultiplier = 1.0f;
        m_Score -= m_impactPenalty;
    }

    public void LevelEnded()
    {
        bUpdateScore = false;
    }

    public float GetCurrentScore()
    {
        return m_CurrentScore;
    }
}
