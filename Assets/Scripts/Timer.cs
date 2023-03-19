using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField] private float startTime;

    [SerializeField] private FinalDeathManager changeCanvas;

    [HideInInspector] public float currentTime;
    private int timeState;
    private bool timerStarted = false;

    // ref var for my TMP text component
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        Instance = this;
        currentTime = startTime;
        timerText.text = currentTime.ToString();
        timerStarted = true;
        timeState = ScoreTracker.Instance.graveGrade;

        if(timeState == 4)
        {
            currentTime = 10;
        }
        else if (timeState == 3)
        {
            currentTime = 15;
        }
        else if (timeState == 2)
        {
            currentTime = 20;
        }
        else
        {
            currentTime = 30;
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
