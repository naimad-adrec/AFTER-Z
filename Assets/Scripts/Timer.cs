using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    [SerializeField] private UnityEvent timeUp;

    [SerializeField] private FinalDeathManager changeCanvas;

    [SerializeField] private float startTime;
    [HideInInspector] public float currentTime;
    private int timeState;
    private bool timerStarted = false;

    //UI Slider Variable
    [SerializeField] private Slider timerSlider;

    private void Start()
    {
        Instance = this;
        currentTime = startTime;
        timerStarted = true;
        timeState = ScoreTracker.Instance.graveGrade;

        if(timeState == 4)
        {
            currentTime = 15;
        }
        else if (timeState == 3)
        {
            currentTime = 20;
        }
        else if (timeState == 2)
        {
            currentTime = 25;
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
                timerSlider.value = currentTime;
                currentTime -= Time.deltaTime;
            }
            else
            {
                timerStarted = false;
                currentTime = 0;
                timeUp.Invoke();
                changeCanvas.ChangeCanvasFinal();
            }
        }
    }
}
