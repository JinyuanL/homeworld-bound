﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    public Text timerLabel;
    public float timeRemaining;
    private float TIME_REMAINING; // saves the timeRemaining value entered in inspector for when the level is restarted
    private bool outOfTime;
    private int minutes;
    private int seconds;
    private bool timerStarted;
    public UnityEvent powerFailure;

    // Use this for initialization
    void Start () {
        outOfTime = false;
        timerStarted = false;
        TIME_REMAINING = timeRemaining;

    }

    public void startTimer()
    {
        timeRemaining = TIME_REMAINING;
        timerStarted = true;
    }

    public void stopTimer()
    {
        timerStarted = false;
    }

    public bool isOutOfTime()
    {
        return outOfTime;
    }

    void Update()
    {
        if (timerStarted)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
            }

            if (timeRemaining < 0)
            {
                stopTimer();
                outOfTime = true;
                powerFailure.Invoke();
                minutes = 0;
                seconds = 0;
            }
            else
            {
                minutes = Mathf.FloorToInt(timeRemaining / 60F);
                seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);
 
            }

            if (timeRemaining >= 0)
            {
                //update the label value
                timerLabel.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
        }
    }
}
