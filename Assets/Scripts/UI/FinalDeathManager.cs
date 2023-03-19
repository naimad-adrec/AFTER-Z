using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalDeathManager : MonoBehaviour
{
    [SerializeField] private Canvas isdeadfinal;
    [SerializeField] private Canvas playerUi;
    [SerializeField] private TextMeshProUGUI graveyardGrade;
    [SerializeField] private TextMeshProUGUI townGrade;

    public int finalTownGrade;
    private void Start()
    {
        isdeadfinal.enabled = false;
    }

    public void ChangeCanvasFinal()
    {
        isdeadfinal.enabled = true;
        playerUi.enabled = false;
        if (ScoreTracker.Instance.graveGrade == 4)
        {
            graveyardGrade.text = "C";
        }
        else if (ScoreTracker.Instance.graveGrade == 3)
        {
            graveyardGrade.text = "B";
        }
        else if (ScoreTracker.Instance.graveGrade == 2)
        {
            graveyardGrade.text = "A";
        }
        else
        {
            graveyardGrade.text = "Z";
        }
        Debug.Log(finalTownGrade);
        if (finalTownGrade == 4)
        {
            townGrade.text = "C";
        }
        else if (finalTownGrade == 3)
        {
            townGrade.text = "B";
        }
        else if (finalTownGrade == 2)
        {
            townGrade.text = "A";
        }
        else
        {
            townGrade.text = "Z";
        }
        Debug.Log(finalTownGrade);
    }


    public void PlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
