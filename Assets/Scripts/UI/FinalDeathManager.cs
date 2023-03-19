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
        Debug.Log(ScoreTracker.Instance.townGrade);
        if (ScoreTracker.Instance.townGrade == 4)
        {
            townGrade.text = "C";
        }
        else if (ScoreTracker.Instance.townGrade == 3)
        {
            townGrade.text = "B";
        }
        else if (ScoreTracker.Instance.townGrade == 2)
        {
            townGrade.text = "A";
        }
        else
        {
            townGrade.text = "Z";
        }
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
