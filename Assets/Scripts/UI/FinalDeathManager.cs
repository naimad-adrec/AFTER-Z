using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalDeathManager : MonoBehaviour
{
    [SerializeField] private Canvas isdeadfinal;
    private TextMeshProUGUI graveyardText;
    private TextMeshProUGUI townText;
    private void Start()
    {
        isdeadfinal.enabled = false;
    }


    public void ChangeCanvasFinal()
    {
        isdeadfinal.enabled = true;
        if(isdeadfinal == enabled)
        {
            //graveyardText.text =
            //townText.text = 
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
