using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_TimerText;
    [SerializeField] float m_timeBeforeTimer = 1.0f;

    [Header("Post Countdown Behaviour")]
    [SerializeField] public UnityEvent CountdownCompleted = new UnityEvent();

    float m_Countdown = 3.5f;
    int m_Time = 3;

    // Update is called once per frame
    void Update()
    {
        if (m_timeBeforeTimer > 0.0f)
        {
            m_timeBeforeTimer -= Time.deltaTime;
            return;
        }

        if (!m_TimerText)
        {
            Destroy(this);
            return;
        }

        m_Countdown -= Time.deltaTime;
        m_Time = (int)Mathf.Clamp(m_Countdown + 0.5f, 0, 3);

        if (m_Time > 0)
        {
            m_TimerText.text = m_Time.ToString();
        }
        else
        {
            m_TimerText.text = "GO!";
            if (m_Countdown <= 0.0f)
            {                
                CountdownCompleted.Invoke();

                Destroy(m_TimerText.gameObject);
                Destroy(this);
            }
        }
    }

 
}
