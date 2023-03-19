using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleManager : MonoBehaviour
{
   public void MainPlayButton()
   {
        SceneManager.LoadScene(1);
   }
    
    public void HowToPlayButton()
    {
        //SceneManager.LoadScene();
    }   
}
