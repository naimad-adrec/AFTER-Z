using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime;

    //public Return_Menu changeCanvas;

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
        timeState = GameObject.Find("Z").GetComponent<Z_Movement>().graveyardGrade;

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
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                timerStarted = false;
                currentTime = 0;
            }
            timerText.text = "Time " + currentTime.ToString("f1");
        }
    }
}
