using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalDeathManager : MonoBehaviour
{
    [SerializeField] private Canvas isdeadfinal;
    private void Start()
    {
        isdeadfinal.enabled = false;
    }


    public void ChangeCanvasFinal()
    {
        isdeadfinal.enabled = true;
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
