using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker Instance
    { get; private set; }

    public int graveGrade;
    public int townGrade;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

}
