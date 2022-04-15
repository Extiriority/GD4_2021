using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
    public Text timer;
    public Text timerBG;
    public Text msTimer;
    public Text msTimerBG;
    public float startTime;
    private bool finished;
    private float time, mSec, sec, min;
    
    private void Start() {
        startTime = Time.time;
    }

    private void Update() {
        if (finished)
            return;
        stopWatch();
    }
    
    private void stopWatch() {
        time = Time.time - startTime;
        min = (int)(time / 60 % 60);
        sec = (int)(time % 60);
        mSec = (int)((time - (int)time) * 100);
        timer.text = $"{min:00}:{sec:00}";
        timerBG.text = $"{min:00}:{sec:00}";
        msTimer.text = $".{mSec:00}";
        msTimerBG.text = $".{mSec:00}";
    }

    public void finish() {
        finished = true;
        timer.color = Color.green;
        msTimer.color = Color.green;
    }
}
