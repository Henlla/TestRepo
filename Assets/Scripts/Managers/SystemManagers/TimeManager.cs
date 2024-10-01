using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public UnityEvent onHourPassed;
    public UnityEvent onMinutePassed;

    private int lastCheckedHour;
    private int lastCheckedMinute;

    void Start()
    {
        lastCheckedHour = DateTime.Now.Hour;
        lastCheckedMinute = DateTime.Now.Minute;
        InvokeRepeating(nameof(CheckTime), 1f, 30f); // Check every 60 seconds
    }

    void CheckTime()
    {
        int currentHour = DateTime.Now.Hour;
        int currentMinute = DateTime.Now.Minute;

        if (currentHour != lastCheckedHour)
        {
            lastCheckedHour = currentHour;
            onHourPassed?.Invoke();
        }

        if (currentMinute != lastCheckedMinute)
        {
            lastCheckedMinute = currentMinute;
            onMinutePassed?.Invoke();
        }
    }
}
