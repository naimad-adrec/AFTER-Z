using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime;

    [SerializeField] private FinalDeathManager changeCanvas;

    private float currentTime;
    private int timeState;
    private bool timerStarted = false;

    // ref var for my TMP text component
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        currentTime = startTime;
        timerText.text = currentTime.ToString();
        timerStarted = true;
        timeState = ScoreTracker.Instance.graveGrade;

        if(timeState == 4)
        {
            currentTime = 60;
        }
        else if (timeState == 3)
        {
            currentTime = 90;
        }
        else if (timeState == 2)
        {
            currentTime = 120;
        }
        else
        {
            currentTime = 180;
        }
    }

    private void Update()
    {
        if (timerStarted)
        {
            
            if (currentTime > 0)
            {
                timerText.text = "Time " + currentTime.ToString("f1");
                currentTime -= Time.deltaTime;
            }
            else
            {
                timerStarted = false;
                changeCanvas.ChangeCanvasFinal();
                currentTime = 0;
            }
        }
    }
}
