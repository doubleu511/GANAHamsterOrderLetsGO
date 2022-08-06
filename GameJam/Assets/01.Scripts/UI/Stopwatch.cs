using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    private string m_Timer = @"00:00:00.000";
    private bool m_IsPlaying = true;
    public float m_TotalSeconds;
    private TextMeshProUGUI m_Text;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (m_IsPlaying)
        {
            m_Timer = StopwatchTimer();
        }

        if (m_Text)
            m_Text.text = m_Timer;
    }

    string StopwatchTimer()
    {
        m_TotalSeconds += Time.deltaTime;
        TimeSpan timespan = TimeSpan.FromSeconds(m_TotalSeconds);
        string timer = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }
}
