using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToScript : MonoBehaviour
{
    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }
}
