using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathManager : MonoBehaviour
{
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private Image Qmark;
    private bool currentStatus;

    //Audio Variables
    private AudioSource aud;
    [SerializeField] private AudioClip groan;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        deathCanvas.enabled = false;
        Qmark.enabled = false;
    }

    private void Update()
    {
        currentStatus = Z_Movement.Instance.deathCanvasStatus;
        if(currentStatus == true)
        {
            ChangeCanvas();
            Invoke("QMarkEnable", 3);
        }
    }

    public void ChangeCanvas()
    {   
        deathCanvas.enabled = true;
        playerCanvas.enabled = false;
    }

    private void QMarkEnable()
    {
        aud.clip = groan;
        Qmark.enabled = true;
        aud.Play();
    }

    public void PlayZombieMode()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
